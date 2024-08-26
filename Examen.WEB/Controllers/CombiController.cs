using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen.WEB.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CombiController : ControllerBase
    {
        private readonly IServiceCombi _serviceCombi;

        public CombiController(IServiceCombi serviceCombi)
        {
            _serviceCombi = serviceCombi;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var combis = _serviceCombi.GetAll();
            return Ok(combis);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var combi = _serviceCombi.GetById(id);
            if (combi == null)
            {
                return NotFound();
            }
            return Ok(combi);
        }

        [HttpPost]
        public IActionResult Create(Combi combi)
        {
            _serviceCombi.Add(combi);
            _serviceCombi.Commit();
            return CreatedAtAction(nameof(GetById), new { id = combi.CombiID }, combi);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Combi combi)
        {
            if (combi == null)
            {
                return BadRequest("Le combi ne peut pas être null.");
            }

            var existingCombi = _serviceCombi.GetById(id);
            if (existingCombi == null)
            {
                return NotFound("Combi non trouvé.");
            }

            // Mettez à jour uniquement les propriétés pertinentes
            existingCombi.PageID = combi.PageID;
            existingCombi.CombiCode = combi.CombiCode;
            existingCombi.Price = combi.Price;
            existingCombi.Discount = combi.Discount;
            existingCombi.DisplayPrice = combi.DisplayPrice;
            // Notez que nous ne mettons pas à jour MenuPage ni Items ici,
            // car ils sont ignorés (JsonIgnore) et probablement gérés séparément.

            try
            {
                _serviceCombi.Update(existingCombi);
                _serviceCombi.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur du serveur : {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var combi = _serviceCombi.GetById(id);
            if (combi == null)
            {
                return NotFound();
            }

            _serviceCombi.Delete(combi);
            _serviceCombi.Commit();
            return NoContent();
        }
    }
}
