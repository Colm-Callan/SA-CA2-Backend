using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SACA2.Data;
using SACA2.Models;

[ApiController]
[Route("api/[controller]")]
public class FixtureController : ControllerBase
{
    private readonly AppDbContext _context;

    public FixtureController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetFixtures()
    {
        // var fixtures = await _context.Fixtures.ToListAsync();
        var fixtures = await _context.Fixtures
            .Include(f => f.HomeTeam)
            .Include(f => f.AwayTeam)
            .Include(f => f.Pitch)
            .ToListAsync();

        var result = fixtures.Select(f => new
        {
            f.Id,
            MatchDate = f.MatchDate,

            // passes names and ids
            HomeTeam = new
            {
                f.HomeTeam.Id,
                f.HomeTeam.Name
            },

            AwayTeam = new
            {
                f.AwayTeam.Id,
                f.AwayTeam.Name
            },

            Pitch = new
            {
                f.Pitch.Id,
                f.Pitch.Name
            }
        });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFixture([FromBody] Fixture fixture)
    {
        _context.Fixtures.Add(fixture);
        await _context.SaveChangesAsync();

        return Ok(fixture);
    }
}