using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Examen.ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen.WEB.Controllers
{
  //  [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController : ControllerBase
    {
        private readonly IServiceOrderDetail _serviceOrderDetail;

        public OrderDetailController(IServiceOrderDetail serviceOrderDetail)
        {
            _serviceOrderDetail = serviceOrderDetail;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_serviceOrderDetail.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var orderDetail = _serviceOrderDetail.GetById(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return Ok(orderDetail);
        }

        [HttpPost]
        public IActionResult Create(OrderDetail orderDetail)
        {
            _serviceOrderDetail.Add(orderDetail);
            _serviceOrderDetail.Commit();
            return CreatedAtAction(nameof(GetById), new { id = orderDetail.OrderDetailID }, orderDetail);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailID)
            {
                return BadRequest();
            }

            var existingOrder = _serviceOrderDetail.GetById(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

           

            existingOrder.DeliveryStatus = orderDetail.DeliveryStatus;
            existingOrder.Price = orderDetail.Price;
            existingOrder.OrderID = orderDetail.OrderID;
            existingOrder.ItemID = orderDetail.ItemID;

            try
            {
                _serviceOrderDetail.Update(existingOrder); // Utilisez l'entité existante ici, pas l'entité `orderDetail`
                _serviceOrderDetail.Commit();
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
            var orderDetail = _serviceOrderDetail.GetById(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _serviceOrderDetail.Delete(orderDetail);
            _serviceOrderDetail.Commit();
            return NoContent();
        }
    }
}
