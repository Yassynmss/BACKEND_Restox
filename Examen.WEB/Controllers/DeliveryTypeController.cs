using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen.WEB.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryTypeController : ControllerBase
    {
        private readonly IServiceDeliveryType _serviceDeliveryType;

        public DeliveryTypeController(IServiceDeliveryType serviceDeliveryType)
        {
            _serviceDeliveryType = serviceDeliveryType;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var types = _serviceDeliveryType.GetAll();
            return Ok(types);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var type = _serviceDeliveryType.GetById(id);
            if (type == null)
            {
                return NotFound();
            }
            return Ok(type);
        }

        [HttpPost]
        public IActionResult Create(DeliveryType type)
        {
            _serviceDeliveryType.Add(type);
            _serviceDeliveryType.Commit();
            return CreatedAtAction(nameof(GetById), new { id = type.DeliveryTypeID }, type);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, DeliveryType type)
        {
            if (id != type.DeliveryTypeID)
            {
                return BadRequest("ID in URL does not match ID in the request body.");
            }

            var existingType = _serviceDeliveryType.GetById(id);
            if (existingType == null)
            {
                return NotFound("Delivery type not found.");
            }

            // Mise à jour des propriétés pertinentes
            existingType.DisplayDesc = type.DisplayDesc;

            try
            {
                _serviceDeliveryType.Update(existingType);
                _serviceDeliveryType.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var type = _serviceDeliveryType.GetById(id);
            if (type == null)
            {
                return NotFound();
            }

            _serviceDeliveryType.Delete(type);
            _serviceDeliveryType.Commit();
            return NoContent();
        }
    }
}
