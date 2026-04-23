using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SACA2.Data;
using SACA2.Models;
using SACA2.Services;

[ApiController]
[Route("api/[controller]")]
public class FixtureController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly FixtureGenerationService _fixtureService;

    public FixtureController(AppDbContext context, FixtureGenerationService fixtureService)
    {
        _context = context;
        _fixtureService = fixtureService;
    }

    [HttpGet]
    public async Task<IActionResult> GetFixtures()
    {
        var fixtures = await _context.Fixtures
            .Include(f => f.HomeTeam)
            .Include(f => f.AwayTeam)
            .Include(f => f.Pitch)
            .ToListAsync();

        var result = fixtures.Select(f => new
        {
            f.Id,
            MatchDate = f.MatchDate,
            HomeTeam = new { f.HomeTeam.Id, f.HomeTeam.Name },
            AwayTeam = new { f.AwayTeam.Id, f.AwayTeam.Name },
            Pitch = new { f.Pitch.Id, f.Pitch.Name }
        });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFixture([FromBody] Fixture fixture)
    {
        // only one match per day
        var existingMatch = await _context.Fixtures
            .FirstOrDefaultAsync(f => f.MatchDate.Date == fixture.MatchDate.Date);

        if (existingMatch != null)
            return BadRequest("A match is already scheduled for this date");

        _context.Fixtures.Add(fixture);
        await _context.SaveChangesAsync();

        return Ok(fixture);
    }

    // Generate random match for a specific date
    [HttpPost("generate/{date}")]
    public async Task<IActionResult> GenerateFixtureForDate(DateTime date)
    {
        var fixture = await _fixtureService.GenerateMatchForDate(date);

        if (fixture == null)
            return BadRequest("Could not generate fixture (match may already exist or insufficient teams/pitches)");

        return Ok(fixture);
    }

    // Generate fixtures for next N days
    [HttpPost("generate-multiple/{days}")]
    public async Task<IActionResult> GenerateFixtures(int days)
    {
        var fixtures = await _fixtureService.GenerateFixturesForDays(days);
        return Ok(new { Count = fixtures.Count, Fixtures = fixtures });
    }
}