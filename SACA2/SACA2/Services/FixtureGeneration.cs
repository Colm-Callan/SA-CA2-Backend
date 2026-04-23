using SACA2.Data;
using SACA2.Models;
using Microsoft.EntityFrameworkCore;

namespace SACA2.Services
{
    public class FixtureGenerationService
    {
        private readonly AppDbContext _context;
        private readonly Random _random = new Random();

        public FixtureGenerationService(AppDbContext context)
        {
            _context = context;
        }

        // Generate one rand match for date
        public async Task<Fixture?> GenerateMatchForDate(DateTime date)
        {

            date = new DateTime(date.Year, date.Month, date.Day, 20, 0, 0, DateTimeKind.Utc);

            // Check if match already exists
            var existingMatch = await _context.Fixtures
                .FirstOrDefaultAsync(f => f.MatchDate.Date == date.Date);

            if (existingMatch != null)
                return null; // Already has a match

            var teams = await _context.Teams.ToListAsync();
            var pitches = await _context.Pitches.ToListAsync();

            if (teams.Count < 2)
                return null; // Need at least 2 teams

            if (!pitches.Any())
                return null; // Need at least 1 pitch

            // Pick 2 rand teams
            var shuffledTeams = teams.OrderBy(x => _random.Next()).Take(2).ToList();
            var homeTeam = shuffledTeams[0];
            var awayTeam = shuffledTeams[1];

            // Pick rand pitch
            var pitch = pitches[_random.Next(pitches.Count)];

            var fixture = new Fixture
            {
                HomeTeamId = homeTeam.Id,
                AwayTeamId = awayTeam.Id,
                PitchId = pitch.Id,
                MatchDate = date
            };

            _context.Fixtures.Add(fixture);
            await _context.SaveChangesAsync();

            return fixture;
        }

        // Generate fixtures for next N days
        public async Task<List<Fixture>> GenerateFixturesForDays(int numberOfDays)
        {
            var fixtures = new List<Fixture>();
            var startDate = DateTime.UtcNow.Date.AddDays(1); // Start tomorrow

            for (int i = 0; i < numberOfDays; i++)
            {
                var matchDate = startDate.AddDays(i);
                var fixture = await GenerateMatchForDate(matchDate);

                if (fixture != null)
                    fixtures.Add(fixture);
            }

            return fixtures;
        }
    }
}