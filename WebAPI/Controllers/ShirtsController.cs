using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Filters.ActionFilters;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController: ControllerBase
    {
        private readonly ApplicationDbContext db;

        public ShirtsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(db.Shirts.ToList());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        public IActionResult GetShirtById(int id)
        {
            return Ok(HttpContext.Items["shirt"]);
        }

        [HttpPost]
        public IActionResult CreateShirt(Shirt shirt)
        {
            db.Shirts.Add(shirt);
            db.SaveChanges();

            return CreatedAtAction(nameof(GetShirtById), new { id = shirt.ShirtId }, shirt);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShirt(int id) 
        {
            return Ok(id);
        }
    }
}
