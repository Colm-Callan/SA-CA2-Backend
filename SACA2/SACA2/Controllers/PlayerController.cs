using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SACA2.Data;
using SACA2.Models;

namespace SACA2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlayerController(AppDbContext context)
        {
            _context = context;
        }

        //Get all the players
        [HttpGet]
        public async Task<IActionResult> GetAllPlayers()
        {
            var players = await _context.Players.ToListAsync();
            return Ok(players);
        }

        // Get player by theirID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
                return NotFound("Player not found");

            return Ok(player);
        }

        // Get all players in the team
        [HttpGet("team/{teamId}")]
        public async Task<IActionResult> GetPlayersByTeam(int teamId)
        {
            var players = await _context.Players
                .Where(p => p.TeamId == teamId)
                .ToListAsync();

            return Ok(players);
        }

        //Create new player
        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] Player player)
        {
            // Check if team is real
            var team = await _context.Teams.FindAsync(player.TeamId);
            if (team == null)
                return BadRequest("Team not found");

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return Ok(player);
        }

        //Update player
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(int id, [FromBody] Player player)
        {
            var existingPlayer = await _context.Players.FindAsync(id);

            if (existingPlayer == null)
                return NotFound("Player not found");

            existingPlayer.Name = player.Name;
            existingPlayer.KitNumber = player.KitNumber;
            existingPlayer.Age = player.Age;
            existingPlayer.Position = player.Position;
            existingPlayer.TeamId = player.TeamId;

            await _context.SaveChangesAsync();

            return Ok(existingPlayer);
        }

        //Delete player
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
                return NotFound("Player not found");

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return Ok("Player deleted successfully");
        }
    }
}