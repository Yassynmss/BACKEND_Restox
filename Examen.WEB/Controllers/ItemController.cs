using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Examen.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IServiceItem _serviceItem;
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

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
        public async Task<IActionResult> Create([FromForm] ItemCreateDto itemDto)
        {
            if (itemDto == null || itemDto.ImageFile == null || itemDto.ImageFile.Length == 0)
            {
                return BadRequest("L'item ou le fichier d'image ne peut pas être null.");
            }

            // Créez le répertoire si nécessaire
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }

            // Générez un nom de fichier unique
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(itemDto.ImageFile.FileName)}"; // Assurez-vous de la sécurité du nom
            var filePath = Path.Combine(_uploadPath, fileName);

            // Enregistrez le fichier
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await itemDto.ImageFile.CopyToAsync(stream);
            }

            // Créez un nouvel item
            var newItem = new Item
            {
                ShortDescription = itemDto.ShortDescription,
                ItemOrder = itemDto.ItemOrder,
                AnimationUrl = fileName,
                PageID = itemDto.PageID,
                CombiID = itemDto.CombiID,
                ItemPriceID = itemDto.ItemPriceID
            };

            try
            {
                _serviceItem.Add(newItem);
                _serviceItem.Commit();
                return CreatedAtAction(nameof(Get), new { id = newItem.itemID }, newItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur du serveur : {ex.Message}");
            }
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

    public class ItemCreateDto
    {
        public int ItemPriceID { get; set; }
        public string ShortDescription { get; set; }
        public int ItemOrder { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ItemType Name { get; set; }
        public int PageID { get; set; }
        public IFormFile ImageFile { get; set; }
        public int CombiID { get; set; }
    }

}







