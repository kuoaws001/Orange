﻿using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
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
        public IActionResult GetShirtById(int id)
        {
            return Ok($"{id}");
        }

        [HttpPost]
        public IActionResult CreateShirt(Shirt shirt)
        {
            return Ok(shirt);
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