using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen.WEB.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemPriceController : ControllerBase
    {
        private readonly IServiceItemPrice _serviceItemPrice;

        public ItemPriceController(IServiceItemPrice serviceItemPrice)
        {
            _serviceItemPrice = serviceItemPrice;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_serviceItemPrice.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var itemPrice = _serviceItemPrice.GetById(id);
            if (itemPrice == null)
            {
                return NotFound();
            }
            return Ok(itemPrice);
        }

        [HttpPost]
        public IActionResult Create(ItemPrice itemPrice)
        {
            _serviceItemPrice.Add(itemPrice);
            _serviceItemPrice.Commit();
            return CreatedAtAction(nameof(GetById), new { id = itemPrice.ItemPriceID }, itemPrice);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] ItemPrice itemPrice)
        {
            if (itemPrice == null)
            {
                return BadRequest("ItemPrice cannot be null.");
            }

            var existingItemPrice = _serviceItemPrice.GetById(id);
            if (existingItemPrice == null)
            {
                return NotFound();
            }

            // Update only the relevant properties
            existingItemPrice.Price = itemPrice.Price;
            existingItemPrice.Discount = itemPrice.Discount;
            existingItemPrice.DisplayPrice = itemPrice.DisplayPrice;
            existingItemPrice.CurrencyID = itemPrice.CurrencyID;

            try
            {
                _serviceItemPrice.Update(existingItemPrice);
                _serviceItemPrice.Commit();
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
            var itemPrice = _serviceItemPrice.GetById(id);
            if (itemPrice == null)
            {
                return NotFound();
            }

            _serviceItemPrice.Delete(itemPrice);
            _serviceItemPrice.Commit();
            return NoContent();
        }
    }
}
