using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Examen.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailModelController : ControllerBase
    {
        private readonly IServiceEmailModel _serviceEmailModel;
        private readonly IConfiguration _config;

        public EmailModelController(IServiceEmailModel serviceEmailModel, IConfiguration config)
        {
            _serviceEmailModel = serviceEmailModel;
            _config = config; // Inject the configuration
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel email)
        {
            if (email == null || string.IsNullOrEmpty(email.Email) || string.IsNullOrEmpty(email.Subject) || string.IsNullOrEmpty(email.Message))
            {
                return BadRequest("Invalid email data.");
            }

            // Enregistrez l'email dans la base de données ici
            await _serviceEmailModel.AddAsync(email); 

            var smtpClient = new SmtpClient(_config["Smtp:Host"])
            {
                Port = int.Parse(_config["Smtp:Port"]),
                Credentials = new NetworkCredential(_config["Smtp:User"], _config["Smtp:Pass"]),
                EnableSsl = true,
            };

            var message = new MailMessage
            {
                From = new MailAddress(_config["Smtp:User"]),
                Subject = email.Subject,
                Body = GenerateEmailBody(email),
                IsBodyHtml = true,
            };

            message.To.Add(email.Email); // Utilisez l'email du client

            try
            {
                await smtpClient.SendMailAsync(message);
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error sending email: {ex.Message}");
            }
        }

        private string GenerateEmailBody(EmailModel email)
        {
            return $@"
            <html>
            <head>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        margin: 20px;
                    }}
                    .header {{
                        text-align: center;
                        padding: 20px;
                    }}
                    .content {{
                        margin: 20px 0;
                    }}
                    .footer {{
                        text-align: center;
                        font-size: small;
                        color: gray;
                    }}
                </style>
            </head>
            <body>
                <div class='header'>
                    <img src='https://static.vecteezy.com/ti/vecteur-libre/p3/22394762-restaurant-resto-nourriture-rechercher-cafe-logo-gratuit-vectoriel.jpg' alt='Logo' style='width: 100px;' />
                    <h2>Bienvenue!</h2>
                </div>
                <div class='content'>
                    <h3>Subject: {email.Subject}</h3>
                    <p>{email.Message}</p>
                </div>
                <div class='footer'>
                    <p>Merci de nous avoir contactés!</p>
                </div>
            </body>
            </html>";
        }
    }
}
