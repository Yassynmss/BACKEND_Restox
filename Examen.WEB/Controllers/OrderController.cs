using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen.WEB.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IServiceOrder _serviceOrder;

        public OrderController(IServiceOrder serviceOrder)
        {
            _serviceOrder = serviceOrder;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_serviceOrder.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _serviceOrder.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            _serviceOrder.Add(order);
            _serviceOrder.Commit();
            return CreatedAtAction(nameof(GetById), new { id = order.OrderID }, order);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Order cannot be null.");
            }

            var existingOrder = _serviceOrder.GetById(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            // Update only relevant properties
            existingOrder.ApplicationUserId = order.ApplicationUserId;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.PaymentMethod = order.PaymentMethod;
            existingOrder.PaymentStatus = order.PaymentStatus;
            existingOrder.DeliveryStatusID = order.DeliveryStatusID;
            existingOrder.DeliveryTypeID = order.DeliveryTypeID;
         //   existingOrder.DatCrea = order.DatCrea;
         //   existingOrder.DatUpt = DateTime.Now;

            try
            {
                _serviceOrder.Update(existingOrder);
                _serviceOrder.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _serviceOrder.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            _serviceOrder.Delete(order);
            _serviceOrder.Commit();
            return NoContent();
        }
    }
}
