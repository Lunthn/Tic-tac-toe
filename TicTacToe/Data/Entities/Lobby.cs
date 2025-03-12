namespace TicTacToe.Data.Entities
{
    public class Lobby
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; } // open/closed
        public string Player1Id { get; set; }
        public User Player1 { get; set; }
        public string Player2Id { get; set; }
        public User Player2 { get; set; }
    }
}
