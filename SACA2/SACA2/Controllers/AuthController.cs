using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SACA2.Data;
using SACA2.Models;
using System;

namespace SACA2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] User user)
        {
            // Check if user exists
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                return BadRequest("User already exists");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Signup successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

            if (existingUser == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { message = "Login successful" });
        }
    }
}