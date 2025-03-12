namespace TicTacToe.Data.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public int LobbyId { get; set; }
        public Lobby Lobby { get; set; }
        public string? WinnerId { get; set; }
        public User Winner { get; set; }
        public string Status { get; set; } // in_progress/draw/winner
        public DateTime DateStarted { get; set; }
    }
}
