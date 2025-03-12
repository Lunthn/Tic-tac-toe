namespace TicTacToe.Data.Entities
{
    public class Move
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public string PlayerId { get; set; }
        public User Player { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public string Symbol { get; set; } // X/O
        public DateTime Timestamp { get; set; }
    }
}
