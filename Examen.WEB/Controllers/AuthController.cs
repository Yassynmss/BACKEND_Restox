using Examen.ApplicationCore.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace Examen.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager; // Ajouté
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager; // Initialisé
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Pseudo);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Issuer"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                // Créer un cookie avec le jeton JWT
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(30)
                };

                Response.Cookies.Append("AuthToken", tokenString, cookieOptions);

                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Pseudo,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.Phone,
                Organization = model.Organization,
                DatCrea = DateTime.UtcNow,
                DatUpt = DateTime.UtcNow,
                IsVerified = model.IsVerified,
                IsLocked = model.IsLocked
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (Enum.TryParse<RoleType>(model.Role, true, out var roleType) && Enum.IsDefined(typeof(RoleType), roleType))
                {
                    var roleName = roleType.ToString(); // Convertir l'enum en nom de rôle

                    // Crée le rôle si nécessaire
                    if (!await _roleManager.RoleExistsAsync(roleName))
                    {
                        var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                        if (!roleResult.Succeeded)
                        {
                            return BadRequest("Failed to create role.");
                        }
                    }

                    // Ajoute le rôle spécifié à l'utilisateur
                    await _userManager.AddToRoleAsync(user, roleName);
                }
                else
                {
                    return BadRequest("Invalid role specified.");
                }

                return Ok();
            }

            return BadRequest(result.Errors);
        }

        public class LoginModel
        {
            public string Pseudo { get; set; }
            public string Password { get; set; }
        }

        public class RegisterModel
        {
            public string Pseudo { get; set; }
            public string Organization { get; set; }
            public string Password { get; set; }
            public string FullName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public DateTime DatCrea { get; set; }
            public DateTime DatUpt { get; set; }
            public bool IsVerified { get; set; }
            public bool IsLocked { get; set; }

            // Nouveau champ pour le rôle
            public string Role { get; set; }
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var roleName = RoleType.CUSTOMER.ToString(); // Nom du rôle à chercher

            var usersInCustomerRole = await _userManager.GetUsersInRoleAsync(roleName);

            if (usersInCustomerRole == null || !usersInCustomerRole.Any())
            {
                return NotFound("No customers found.");
            }

            var customerList = usersInCustomerRole.Select(user => new
            {
                user.Id,
                user.UserName,
                user.FullName,
                user.Email,
                user.PhoneNumber,
                user.Organization
            }).ToList();

            return Ok(customerList);
        }
    }
}
