namespace SACA2.Models
{
    public class Fixture
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public Team? HomeTeam { get; set; }
        public Team? AwayTeam { get; set; }

        public int PitchId { get; set; }
        public Pitch? Pitch { get; set; }

        public DateTime MatchDate { get; set; }
    }
}