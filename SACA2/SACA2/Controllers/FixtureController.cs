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
        var fixtures = await _context.Fixtures.ToListAsync();
        return Ok(fixtures);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFixture([FromBody] Fixture fixture)
    {
        _context.Fixtures.Add(fixture);
        await _context.SaveChangesAsync();

        return Ok(fixture);
    }
}