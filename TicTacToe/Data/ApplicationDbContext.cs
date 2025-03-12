using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Data.Entities;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Lobby> Lobbies { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Move> Moves { get; set; }
    public DbSet<Leaderboard> Leaderboards { get; set; }
    public DbSet<AdminLog> AdminLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.UserName).IsUnique();
            entity.HasIndex(u => u.Email).IsUnique();
        });

        modelBuilder.Entity<Lobby>(entity =>
        {
            entity.HasIndex(l => l.Code).IsUnique();
            entity.HasOne(l => l.Player1).WithMany().HasForeignKey(l => l.Player1Id).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(l => l.Player2).WithMany().HasForeignKey(l => l.Player2Id).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasOne(g => g.Lobby).WithMany().HasForeignKey(g => g.LobbyId);
            entity.HasOne(g => g.Winner).WithMany().HasForeignKey(g => g.WinnerId).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Move>(entity =>
        {
            entity.HasOne(m => m.Game).WithMany().HasForeignKey(m => m.GameId);
            entity.HasOne(m => m.Player).WithMany().HasForeignKey(m => m.PlayerId);
        });

        modelBuilder.Entity<Leaderboard>(entity =>
        {
            entity.HasOne(lb => lb.User).WithMany().HasForeignKey(lb => lb.UserId);
        });

        modelBuilder.Entity<AdminLog>(entity =>
        {
            entity.HasOne(al => al.Admin).WithMany().HasForeignKey(al => al.AdminId);
            entity.HasOne(al => al.Target).WithMany().HasForeignKey(al => al.TargetId);
        });
    }
}