namespace ELearning.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }


    // ============================================================
    // FOREIGN KEY: User
    // ============================================================
    //
    // Relationship:
    //
    //      User 1 ─────────── * RefreshToken
    //
    // One User can have MANY refresh tokens.
    //
    // Example:
    //
    //      John
    //        ├── Laptop token
    //        ├── Phone token
    //        └── Tablet token
    //
    // UserId → User.Id
    //
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;


    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RevokedAt { get; set; }

    public string? ReplacedByToken { get; set; }


    // These are calculated properties.
    //
    // They are not necessarily database columns.
    //
    public bool IsRevoked => RevokedAt.HasValue;

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
}