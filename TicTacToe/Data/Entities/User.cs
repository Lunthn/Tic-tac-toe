using Microsoft.AspNetCore.Identity;

namespace TicTacToe.Data.Entities
{
    public class User : IdentityUser
    {
        public string Role { get; set; } 
        public int NumberOfWins { get; set; }

    }
}
