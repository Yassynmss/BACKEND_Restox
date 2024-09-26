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
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
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

            // Gestion de l'upload de la photo
            if (model.Photo != null)
            {
                var uploadsFolder = Path.Combine("wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder); // Crée le dossier s'il n'existe pas

                var fileName = $"{Guid.NewGuid()}_{model.Photo.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(fileStream);
                }

                user.Photo = $"/uploads/{fileName}"; 
            }

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (Enum.TryParse<RoleType>(model.Role, true, out var roleType) && Enum.IsDefined(typeof(RoleType), roleType))
                {
                    var roleName = roleType.ToString();

                    if (!await _roleManager.RoleExistsAsync(roleName))
                    {
                        var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                        if (!roleResult.Succeeded)
                        {
                            return BadRequest("Failed to create role.");
                        }
                    }

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
            public string Role { get; set; }

            // Ajout du champ pour la photo
            public IFormFile? Photo { get; set; }
            public virtual ICollection<Adress>? Adresses { get; set; } = new List<Adress>();

        }


        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var roleName = RoleType.CUSTOMER.ToString();

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
                user.Organization,
                user.Photo // Inclure la photo dans la réponse
            }).ToList();

            return Ok(customerList);
        }

        [HttpGet("restochefs")]
        public async Task<IActionResult> GetRestoChef()
        {
            var roleName = RoleType.RESTOCHEF.ToString();

            var usersInChefRole = await _userManager.GetUsersInRoleAsync(roleName);

            if (usersInChefRole == null || !usersInChefRole.Any())
            {
                return NotFound("No chefs found.");
            }

            var chefList = usersInChefRole.Select(user => new
            {
                user.Id,
                user.UserName,
                user.FullName,
                user.Email,
                user.PhoneNumber,
                user.Organization,
                user.Photo
            }).ToList();

            return Ok(chefList);
        }
        [HttpGet("count")]
        public async Task<IActionResult> GetNumberOfUsersByType([FromQuery] string type)
        {
            if (Enum.TryParse<RoleType>(type, true, out var roleType) && Enum.IsDefined(typeof(RoleType), roleType))
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(roleType.ToString());
                if (usersInRole == null)
                {
                    return NotFound($"No users found for the role: {type}");
                }

                return Ok(usersInRole.Count);
            }

            return BadRequest("Invalid user type specified.");
        }


    }


}
