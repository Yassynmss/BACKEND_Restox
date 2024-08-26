using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen.WEB.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly IServiceLanguage _serviceLanguage;

        public LanguageController(IServiceLanguage serviceLanguage)
        {
            _serviceLanguage = serviceLanguage;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_serviceLanguage.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var language = _serviceLanguage.GetById(id);
            if (language == null)
            {
                return NotFound();
            }
            return Ok(language);
        }

        [HttpPost]
        public IActionResult Create(Language language)
        {
            _serviceLanguage.Add(language);
            _serviceLanguage.Commit();
            return CreatedAtAction(nameof(GetById), new { id = language.LanguageID }, language);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Language language)
        {
            if (language == null)
            {
                return BadRequest("Language cannot be null.");
            }

            var existingLanguage = _serviceLanguage.GetById(id);
            if (existingLanguage == null)
            {
                return NotFound();
            }

            // Update only relevant properties
            existingLanguage.ShortDescription = language.ShortDescription;
            existingLanguage.ISOCode = language.ISOCode;
            existingLanguage.DisplayCode = language.DisplayCode;

            try
            {
                _serviceLanguage.Update(existingLanguage);
                _serviceLanguage.Commit();
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
            var language = _serviceLanguage.GetById(id);
            if (language == null)
            {
                return NotFound();
            }

            _serviceLanguage.Delete(language);
            _serviceLanguage.Commit();
            return NoContent();
        }
    }
}
