namespace TicTacToe.Data.Entities
{
    public class Leaderboard
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int Position { get; set; }
    }
}
