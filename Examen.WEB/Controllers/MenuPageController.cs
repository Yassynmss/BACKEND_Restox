using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen.WEB.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MenuPageController : ControllerBase
    {
        private readonly IServiceMenuPage _serviceMenuPage;

        public MenuPageController(IServiceMenuPage serviceMenuPage)
        {
            _serviceMenuPage = serviceMenuPage;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_serviceMenuPage.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var menuPage = _serviceMenuPage.GetById(id);
            if (menuPage == null)
            {
                return NotFound();
            }
            return Ok(menuPage);
        }

        [HttpPost]
        public IActionResult Create(MenuPage menuPage)
        {
            _serviceMenuPage.Add(menuPage);
            _serviceMenuPage.Commit();
            return CreatedAtAction(nameof(GetById), new { id = menuPage.MenuPageID }, menuPage);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] MenuPage menuPage)
        {
            if (menuPage == null)
            {
                return BadRequest("MenuPage cannot be null.");
            }

            var existingMenuPage = _serviceMenuPage.GetById(id);
            if (existingMenuPage == null)
            {
                return NotFound();
            }

            // Update only the relevant properties
            existingMenuPage.ShortDescription = menuPage.ShortDescription;
            existingMenuPage.HtmlDescription = menuPage.HtmlDescription;
            existingMenuPage.PageOrder = menuPage.PageOrder;
            existingMenuPage.AnimationUrl = menuPage.AnimationUrl;
            existingMenuPage.MenuID = menuPage.MenuID;

            try
            {
                _serviceMenuPage.Update(existingMenuPage);
                _serviceMenuPage.Commit();
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
            var menuPage = _serviceMenuPage.GetById(id);
            if (menuPage == null)
            {
                return NotFound();
            }

            _serviceMenuPage.Delete(menuPage);
            _serviceMenuPage.Commit();
            return NoContent();
        }
    }
}
