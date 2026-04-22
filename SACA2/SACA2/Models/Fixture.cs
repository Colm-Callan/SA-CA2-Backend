namespace SACA2.Models
{
    public class Fixture
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public DateTime MatchDate { get; set; } = DateTime.UtcNow;


    }
}