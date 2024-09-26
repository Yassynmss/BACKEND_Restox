//using Examen.ApplicationCore.Domain;
//using Examen.ApplicationCore.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Examen.WEB.Controllers
//{
//    // [Authorize]
//    [ApiController]
//    [Route("api/[controller]")]
//    public class BizAccountController : ControllerBase
//    {
//        private readonly IServiceAccount _serviceBizAccount;

//        public BizAccountController(IServiceAccount serviceBizAccount)
//        {
//            _serviceBizAccount = serviceBizAccount;
//        }

//        [HttpGet]
//        public IActionResult Get()
//        {
//            try
//            {
//                var bizAccounts = _serviceBizAccount.GetAll();
//                return Ok(bizAccounts);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Erreur du serveur : {ex.Message}");
//            }
//        }

//        [HttpGet("{id}")]
//        public IActionResult GetById(int id)
//        {
//            try
//            {
//                var bizAccount = _serviceBizAccount.GetById(id);
//                if (bizAccount == null)
//                {
//                    return NotFound();
//                }
//                return Ok(bizAccount);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Erreur du serveur : {ex.Message}");
//            }
//        }
//        [HttpPost]
//        public IActionResult Create([FromBody] BizAccount bizAccount)
//        {
//            // Vérifie si le corps de la requête est null
//            if (bizAccount is null)
//            {
//                return BadRequest("Le corps de la requête ne peut pas être null.");
//            }

//            // Vérifie si le modèle est valide
//            if (!ModelState.IsValid)
//            {
//                return BadRequest("Le modèle fourni est invalide.");
//            }

//            try
//            {
//                // Ajoute le nouveau BizAccount
//                _serviceBizAccount.Add(bizAccount);
//                _serviceBizAccount.Commit();

//                // Retourne l'ID du nouveau BizAccount avec une réponse 201 Created
//                return CreatedAtAction(nameof(GetById), new { id = bizAccount.BizAccountID }, bizAccount);
//            }
//            catch (DbUpdateException dbEx)
//            {
//                // Capture les exceptions liées à la base de données
//                return StatusCode(500, $"Erreur de la base de données : {dbEx.Message}");
//            }
//            catch (Exception ex)
//            {
//                // Capture et retourne une erreur serveur 500 avec un message d'erreur détaillé
//                return StatusCode(500, $"Erreur du serveur : {ex.Message}");
//            }
//        }



//        [HttpPut("{id}")]
//        public IActionResult Edit(int id, [FromBody] BizAccount bizAccount)
//        {
//            if (bizAccount == null)
//            {
//                return BadRequest("Le corps de la requête ne peut pas être null.");
//            }

//            try
//            {
//                var existingBizAccount = _serviceBizAccount.GetById(id);
//                if (existingBizAccount == null)
//                {
//                    return NotFound();
//                }

//                // Mettez à jour uniquement les propriétés pertinentes
//                existingBizAccount.Pseudo = bizAccount.Pseudo;
//                existingBizAccount.Organization = bizAccount.Organization;
//                existingBizAccount.Password = bizAccount.Password;
//                existingBizAccount.Email = bizAccount.Email;
//                existingBizAccount.DatCrea = bizAccount.DatCrea;
//                existingBizAccount.DatUpt = bizAccount.DatUpt;
//                existingBizAccount.IsVerified = bizAccount.IsVerified;
//                existingBizAccount.IsLocked = bizAccount.IsLocked;

//                _serviceBizAccount.Update(existingBizAccount);
//                _serviceBizAccount.Commit();
//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Erreur du serveur : {ex.Message}");
//            }
//        }

//        [HttpDelete("{id}")]
//        public IActionResult Delete(int id)
//        {
//            try
//            {
//                var bizAccount = _serviceBizAccount.GetById(id);
//                if (bizAccount == null)
//                {
//                    return NotFound();
//                }

//                _serviceBizAccount.Delete(bizAccount);
//                _serviceBizAccount.Commit();
//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Erreur du serveur : {ex.Message}");
//            }
//        }
//    }
//}
