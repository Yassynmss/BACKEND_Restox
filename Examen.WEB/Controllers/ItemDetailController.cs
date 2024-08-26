using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen.WEB.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemDetailController : ControllerBase
    {
        private readonly IServiceItemDetail _serviceItemDetail;

        public ItemDetailController(IServiceItemDetail serviceItemDetail)
        {
            _serviceItemDetail = serviceItemDetail;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_serviceItemDetail.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var itemDetail = _serviceItemDetail.GetById(id);
            if (itemDetail == null)
            {
                return NotFound();
            }
            return Ok(itemDetail);
        }

        [HttpPost]
        public IActionResult Create(ItemDetail itemDetail)
        {
            _serviceItemDetail.Add(itemDetail);
            _serviceItemDetail.Commit();
            return CreatedAtAction(nameof(Get), new { id = itemDetail.ItemDetailID }, itemDetail);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] ItemDetail itemDetail)
        {
            if (itemDetail == null)
            {
                return BadRequest("ItemDetail ne peut pas être null.");
            }

            var existingItemDetail = _serviceItemDetail.GetById(id);
            if (existingItemDetail == null)
            {
                return NotFound();
            }

            // Update only relevant properties
            existingItemDetail.Description = itemDetail.Description;
            existingItemDetail.HtmlDescription = itemDetail.HtmlDescription;
            existingItemDetail.ItemID = itemDetail.ItemID;
            existingItemDetail.LanguageID = itemDetail.LanguageID;

            try
            {
                _serviceItemDetail.Update(existingItemDetail);
                _serviceItemDetail.Commit();
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
            var itemDetail = _serviceItemDetail.GetById(id);
            if (itemDetail == null)
            {
                return NotFound();
            }

            _serviceItemDetail.Delete(itemDetail);
            _serviceItemDetail.Commit();
            return NoContent();
        }
    }
}
