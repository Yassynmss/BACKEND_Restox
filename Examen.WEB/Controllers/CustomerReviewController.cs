using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examen.WEB.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerReviewController : ControllerBase
    {
        private readonly IServiceCustomerReview _serviceCustomerReview;

        public CustomerReviewController(IServiceCustomerReview serviceCustomerReview)
        {
            _serviceCustomerReview = serviceCustomerReview;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var reviews = _serviceCustomerReview.GetAll();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var review = _serviceCustomerReview.GetById(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPost]
        public IActionResult Create(CustomerReview review)
        {
            _serviceCustomerReview.Add(review);
            _serviceCustomerReview.Commit();
            return CreatedAtAction(nameof(GetById), new { id = review.CustomerReviewID }, review);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CustomerReview review)
        {
            if (review == null)
            {
                return BadRequest("L'évaluation ne peut pas être null.");
            }

            if (id != review.CustomerReviewID)
            {
                return BadRequest(new { message = "L'ID dans l'URL ne correspond pas à l'ID dans le corps de la requête." });
            }

            var existingReview = _serviceCustomerReview.GetById(id);
            if (existingReview == null)
            {
                return NotFound("Évaluation non trouvée.");
            }

            // Mettez à jour uniquement les propriétés pertinentes
            existingReview.Comment = review.Comment;
            existingReview.Rate = review.Rate;
            existingReview.IsVerified = review.IsVerified;
            existingReview.DatUpt = DateTime.UtcNow;  // Mettre à jour la date de modification

            try
            {
                _serviceCustomerReview.Update(existingReview);
                _serviceCustomerReview.Commit();
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
            var review = _serviceCustomerReview.GetById(id);
            if (review == null)
            {
                return NotFound();
            }

            _serviceCustomerReview.Delete(review);
            _serviceCustomerReview.Commit();
            return NoContent();
        }
    }
}
