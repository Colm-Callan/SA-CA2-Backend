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
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            var existingTeams = await _context.Teams
                .Where(t => t.Id != team.Id)
                .ToListAsync();

            var pitches = await _context.Pitches.ToListAsync();

            if (!pitches.Any())
                return BadRequest("No pitches available");

            var fixtures = new List<Fixture>();
            int pitchIndex = 0;

            foreach (var opponent in existingTeams)
            {
                var pitch = pitches[pitchIndex % pitches.Count];

                var matchTime = DateTime.UtcNow.AddHours(fixtures.Count);

                // CHECK 2 avoid dupe match
                var exists = await _context.Fixtures.AnyAsync(f =>
                    (f.HomeTeamId == team.Id && f.AwayTeamId == opponent.Id) ||
                    (f.HomeTeamId == opponent.Id && f.AwayTeamId == team.Id));

                if (exists)
                    continue;

                // CHECK 4 pitch availability at that time
                var pitchBusy = await _context.Fixtures.AnyAsync(f =>
                    f.PitchId == pitch.Id &&
                    f.MatchDate == matchTime);

                if (pitchBusy)
                    continue;

                fixtures.Add(new Fixture
                {
                    HomeTeamId = team.Id,
                    AwayTeamId = opponent.Id,
                    PitchId = pitch.Id,
                    MatchDate = matchTime
                });

                pitchIndex++;
            }

            _context.Fixtures.AddRange(fixtures);
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