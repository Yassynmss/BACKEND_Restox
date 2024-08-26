using Microsoft.AspNetCore.Mvc;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;

namespace Examen.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryStatusController : ControllerBase
    {
        private readonly IServiceDeliveryStatus _serviceDeliveryStatus;

        public DeliveryStatusController(IServiceDeliveryStatus serviceDeliveryStatus)
        {
            _serviceDeliveryStatus = serviceDeliveryStatus;
        }

        // GET: api/DeliveryStatus
        [HttpGet]
        public IActionResult Get()
        {
            var deliveryStatuses = _serviceDeliveryStatus.GetAll();
            return Ok(deliveryStatuses);
        }

        // GET: api/DeliveryStatus/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            var deliveryStatus = _serviceDeliveryStatus.GetById(id);
            if (deliveryStatus == null)
            {
                return NotFound();
            }

            return Ok(deliveryStatus);
        }

        // POST: api/DeliveryStatus
        [HttpPost]
        public IActionResult Create(DeliveryStatus deliveryStatus)
        {
            if (deliveryStatus == null || string.IsNullOrWhiteSpace(deliveryStatus.DisplayDesc))
            {
                return BadRequest("Invalid delivery status data.");
            }

            try
            {
                _serviceDeliveryStatus.Add(deliveryStatus);
                _serviceDeliveryStatus.Commit();
                return CreatedAtAction(nameof(GetById), new { id = deliveryStatus.DeliveryStatusID }, deliveryStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/DeliveryStatus/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, DeliveryStatus deliveryStatus)
        {
            if (id != deliveryStatus.DeliveryStatusID)
            {
                return BadRequest("ID in URL does not match ID in body.");
            }

            var existingStatus = _serviceDeliveryStatus.GetById(id);
            if (existingStatus == null)
            {
                return NotFound("Delivery status not found.");
            }

            // Update the relevant properties
            existingStatus.DisplayDesc = deliveryStatus.DisplayDesc;

            try
            {
                _serviceDeliveryStatus.Update(existingStatus);
                _serviceDeliveryStatus.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/DeliveryStatus/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deliveryStatus = _serviceDeliveryStatus.GetById(id);
            if (deliveryStatus == null)
            {
                return NotFound("Delivery status not found.");
            }

            try
            {
                _serviceDeliveryStatus.Delete(deliveryStatus);
                _serviceDeliveryStatus.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
