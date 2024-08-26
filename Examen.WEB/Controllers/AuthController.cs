using Examen.ApplicationCore.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
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
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            var user = new ApplicationUser { UserName = model.Pseudo, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Supprimer la ligne ci-dessous pour éviter l'authentification automatique
                // await _signInManager.SignInAsync(user, false);

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
        [Key]
        public int BizAccountID { get; set; }

        public string Pseudo { get; set; }
        public string Organization { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DatCrea { get; set; }
        public DateTime DatUpt { get; set; }
        public bool IsVerified { get; set; }
        public bool IsLocked { get; set; }
        [JsonIgnore]
        // Relation avec Adress avec suppression en cascade
        public virtual ICollection<Adress>? Adresses { get; set; } = new List<Adress>();

        // Propriété de navigation pour la relation avec Menu
        [JsonIgnore]
        public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

        [JsonIgnore]
        public virtual ICollection<Order>? Orders { get; set; }
    }
}

