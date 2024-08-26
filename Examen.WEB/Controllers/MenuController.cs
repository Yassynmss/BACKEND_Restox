using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen.WEB.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IServiceMenu _serviceMenu;

        public MenuController(IServiceMenu serviceMenu)
        {
            _serviceMenu = serviceMenu;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var menus = _serviceMenu.GetAll();
            return Ok(menus);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var menu = _serviceMenu.GetById(id);
            if (menu == null)
            {
                return NotFound();
            }
            return Ok(menu);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Menu menu)
        {
            if (menu == null)
            {
                return BadRequest("Le menu ne peut pas être null.");
            }

            var existingBizAccount = _serviceMenu.GetBizAccountById(menu.BizAccountID);
            if (existingBizAccount == null)
            {
                return BadRequest("BizAccountID invalide. Aucune compte de ce type trouvé.");
            }

            try
            {
                _serviceMenu.Add(menu);
                _serviceMenu.Commit();
                return CreatedAtAction(nameof(GetById), new { id = menu.MenuID }, menu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur du serveur : {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Menu menu)
        {
            if (menu == null)
            {
                return BadRequest("Menu cannot be null.");
            }

            var existingMenu = _serviceMenu.GetById(id);
            if (existingMenu == null)
            {
                return NotFound();
            }

            // Update only the relevant properties
            existingMenu.Title = menu.Title;
            existingMenu.HtmlDescription = menu.HtmlDescription;
            existingMenu.BizAccountID = menu.BizAccountID;

            try
            {
                _serviceMenu.Update(existingMenu);
                _serviceMenu.Commit();
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
            var menu = _serviceMenu.GetById(id);
            if (menu == null)
            {
                return NotFound();
            }

            _serviceMenu.Delete(menu);
            _serviceMenu.Commit();
            return NoContent();
        }
    }
}
