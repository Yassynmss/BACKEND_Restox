using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen.WEB.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IServiceCustomer _serviceCustomer;

        public CustomerController(IServiceCustomer serviceCustomer)
        {
            _serviceCustomer = serviceCustomer;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = _serviceCustomer.GetAll();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var customer = _serviceCustomer.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _serviceCustomer.Add(customer);
            _serviceCustomer.Commit();
            return CreatedAtAction(nameof(GetById), new { id = customer.CustomerID }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Le client ne peut pas être null.");
            }

            if (id != customer.CustomerID)
            {
                return BadRequest(new { message = "L'ID dans l'URL ne correspond pas à l'ID dans le corps de la requête." });
            }

            var existingCustomer = _serviceCustomer.GetById(id);
            if (existingCustomer == null)
            {
                return NotFound("Client non trouvé.");
            }

            // Mettez à jour uniquement les propriétés pertinentes
            existingCustomer.Pseudo = customer.Pseudo;
            existingCustomer.Email = customer.Email;
            existingCustomer.Password = customer.Password;
            existingCustomer.FullName = customer.FullName;
            existingCustomer.IsVerified = customer.IsVerified;
            existingCustomer.IsLocked = customer.IsLocked;
            existingCustomer.LastConnection = customer.LastConnection;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.AdressID = customer.AdressID;
            existingCustomer.DatUpt = DateTime.UtcNow;  // Mettre à jour la date de modification

            try
            {
                _serviceCustomer.Update(existingCustomer);
                _serviceCustomer.Commit();
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
            var customer = _serviceCustomer.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _serviceCustomer.Delete(customer);
            _serviceCustomer.Commit();
            return NoContent();
        }
    }
}
