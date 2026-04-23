using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SACA2.Data;
using SACA2.Models;

namespace SACA2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamController(AppDbContext context)
        {
            _context = context;
        }

        //Get all teams
        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            var teams = await _context.Teams.ToListAsync();
            return Ok(teams);
        }

        // Get team by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            
            if (team == null)
                return NotFound("Team not found");

            return Ok(team);
        }

        // create new team
        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] Team team)
        {
            // check for dupe teams
            if (await _context.Teams.AnyAsync(t => t.Name == team.Name))
            {
                // return good error mesage
                return Conflict($"A team with the name '{team.Name}' already exists.");
            }

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return Ok(team);
        }

        // Update team
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] Team team)
        {
            var existingTeam = await _context.Teams.FindAsync(id);
            
            if (existingTeam == null)
                return NotFound("Team not found");

            existingTeam.Name = team.Name;
            //existingTeam.Logo = team.Logo;
            existingTeam.Wins = team.Wins;
            existingTeam.Draws = team.Draws;
            existingTeam.Losses = team.Losses;
            existingTeam.Points = team.Points;

            await _context.SaveChangesAsync();

            return Ok(existingTeam);
        }

        //Delete team
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            
            if (team == null)
                return NotFound("Team not found");

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return Ok("Team deleted successfully");
        }
    }
}