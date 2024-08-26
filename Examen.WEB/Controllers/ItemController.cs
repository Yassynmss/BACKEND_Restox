using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen.WEB.Controllers
{
  //  [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IServiceItem _serviceItem;

        public ItemController(IServiceItem serviceItem)
        {
            _serviceItem = serviceItem;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_serviceItem.GetAll());
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            _serviceItem.Add(item);
            _serviceItem.Commit();
            return CreatedAtAction(nameof(Get), new { id = item.ItemID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Item item)
        {
            if (item == null)
            {
                return BadRequest("L'item ne peut pas être null.");
            }

            var existingItem = _serviceItem.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            // Mettez à jour uniquement les propriétés pertinentes
            existingItem.ShortDescription = item.ShortDescription;
            existingItem.ItemOrder = item.ItemOrder;
            existingItem.AnimationUrl = item.AnimationUrl;
            existingItem.PageID = item.PageID;
            existingItem.CombiID = item.CombiID;

            try
            {
                _serviceItem.Update(existingItem);
                _serviceItem.Commit();
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
            var item = _serviceItem.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            _serviceItem.Delete(item);
            _serviceItem.Commit();
            return NoContent();
        }
    }
}
