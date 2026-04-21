namespace SACA2.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int KitNumber { get; set; }
        public int Age { get; set; }
        public string Position { get; set; } = string.Empty;
        public int TeamId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}