using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Examen.WEB.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAngularOrigin")]
    public class AdressController : ControllerBase
    {
        private readonly IServiceAdress _serviceAdress;

        public AdressController(IServiceAdress serviceAdress)
        {
            _serviceAdress = serviceAdress;
        }

        [HttpGet]
        
        public IActionResult Get()
        {
            try
            {
                var addresses = _serviceAdress.GetAll();
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur du serveur : {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Adress adress)
        {
            if (adress == null)
            {
                return BadRequest("Adresse invalide.");
            }

            try
            {
                _serviceAdress.Add(adress);
                _serviceAdress.Commit();
                return CreatedAtAction(nameof(Get), new { id = adress.AdressID }, adress);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur du serveur : {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Adress adress)
        {
            if (adress == null)
            {
                return BadRequest("L'adresse ne peut pas être null.");
            }

            var existingAdress = _serviceAdress.GetById(id);
            if (existingAdress == null)
            {
                return NotFound();
            }

            try
            {
                existingAdress.Line1 = adress.Line1;
                existingAdress.Line2 = adress.Line2;
                existingAdress.Ville = adress.Ville;

                _serviceAdress.Update(existingAdress);
                _serviceAdress.Commit();
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
            var adress = _serviceAdress.GetById(id);
            if (adress == null)
            {
                return NotFound();
            }

            try
            {
                _serviceAdress.Delete(adress);
                _serviceAdress.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur du serveur : {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetAdressById(int id)
        {
            // Fetch the address by id
            var existingAdress = _serviceAdress.GetById(id);

            // Check if the address was found
            if (existingAdress == null)
            {
                return NotFound();
            }

            // Return the address as a JSON response
            return Ok(existingAdress);
        }

        /*    [HttpPut("EditByLine1")]
            public IActionResult EditAdressByLine1([FromBody] AdressLine1UpdateDto updateDto)
            {
                if (updateDto == null || string.IsNullOrEmpty(updateDto.OldLine1) || string.IsNullOrEmpty(updateDto.NewLine1))
                {
                    return BadRequest("Les paramètres oldLine1 et newLine1 ne peuvent pas être vides.");
                }

                try
                {
                    _serviceAdress.EditAdressByLine1(updateDto.OldLine1, updateDto.NewLine1);
                    _serviceAdress.Commit();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erreur du serveur : {ex.Message}");
                }
            }

            [HttpGet("GetAllLine1")]
            public IActionResult GetAllLine1()
            {
                try
                {
                    var line1List = _serviceAdress.GetAllLine1();
                    return Ok(line1List);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erreur du serveur : {ex.Message}");
                }
             */
    }
       
    }
