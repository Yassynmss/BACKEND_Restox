using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            var existingApplicationUser = _serviceMenu.GetApplicationUserById(menu.ApplicationUserID);
            if (existingApplicationUser == null)
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
            existingMenu.ApplicationUserID = menu.ApplicationUserID;

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
        [HttpGet("get-all-menus")]
        public async Task<IActionResult> GetAllMenus()
        {
            try
            {
                // Fetch all menus with their corresponding user (chef)
                var menus = await _serviceMenu.GetAllMenusWithUsersAsync();

                if (menus == null || !menus.Any())
                {
                    return NotFound();
                }

                // Map the result to include chef full name (ApplicationUserFullName)
                var result = menus.Select(menu => new
                {
                    menu.MenuID,
                    menu.Title,
                    menu.HtmlDescription,
                    ApplicationUserFullName = menu.ApplicationUser.FullName, // Chef's full name
                    ApplicationUserID = menu.ApplicationUserID
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }
        [HttpGet("get-menu/{menuId}")]
        public async Task<IActionResult> GetMenuById(int menuId)
        {
            try
            {
                var menu = await _serviceMenu.GetMenuByIdWithUserAsync(menuId);

                if (menu == null)
                {
                    return NotFound();
                }

                var result = new
                {
                    menu.MenuID,
                    menu.Title,
                    menu.HtmlDescription,
                    ApplicationUserFullName = menu.ApplicationUser.FullName, // Return the full name of the user
                    ApplicationUserID = menu.ApplicationUserID
                };

                return Ok(result);
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
