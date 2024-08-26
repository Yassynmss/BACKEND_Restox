using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen.WEB.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly IServiceCurrency _serviceCurrency;

        public CurrencyController(IServiceCurrency serviceCurrency)
        {
            _serviceCurrency = serviceCurrency;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var currencies = _serviceCurrency.GetAll();
            return Ok(currencies);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var currency = _serviceCurrency.GetById(id);
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }

        [HttpPost]
        public IActionResult Create(Currency currency)
        {
            _serviceCurrency.Add(currency);
            _serviceCurrency.Commit();
            return CreatedAtAction(nameof(GetById), new { id = currency.CurrencyID }, currency);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Currency currency)
        {
            if (currency == null)
            {
                return BadRequest("La devise ne peut pas être null.");
            }

            if (id != currency.CurrencyID)
            {
                return BadRequest(new { message = "L'ID dans l'URL ne correspond pas à l'ID dans le corps de la requête." });
            }

            var existingCurrency = _serviceCurrency.GetById(id);
            if (existingCurrency == null)
            {
                return NotFound("Devise non trouvée.");
            }

            // Mettez à jour uniquement les propriétés pertinentes
            existingCurrency.ShortDescription = currency.ShortDescription;
            existingCurrency.MoneyCode = currency.MoneyCode;

            try
            {
                _serviceCurrency.Update(existingCurrency);
                _serviceCurrency.Commit();
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
            var currency = _serviceCurrency.GetById(id);
            if (currency == null)
            {
                return NotFound();
            }

            _serviceCurrency.Delete(currency);
            _serviceCurrency.Commit();
            return NoContent();
        }
    }
}
