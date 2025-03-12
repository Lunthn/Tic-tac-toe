namespace TicTacToe.Data.Entities
{
    public class AdminLog
    {
        public int Id { get; set; }
        public string AdminId { get; set; }
        public User Admin { get; set; }
        public string TargetId { get; set; }
        public User Target { get; set; }
        public string Action { get; set; } // e.g., block user
        public DateTime Timestamp { get; set; }
    }
}
