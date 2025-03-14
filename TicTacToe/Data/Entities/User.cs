namespace TicTacToe
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // player or admin
        public int NumberOfWins { get; set; }
    }
}