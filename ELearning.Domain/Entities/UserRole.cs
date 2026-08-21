namespace ELearning.Domain.Entities;

public class UserRole
{
    // ============================================================
    // FOREIGN KEY: User
    // ============================================================
    //
    // UserId points to:
    //
    //      User.Id
    //
    // This creates:
    //
    //      User 1 ─────────── * UserRole
    //
    public Guid UserId { get; set; }

    // Navigation property:
    //
    //      UserRole → User
    //
    public User User { get; set; } = null!;


    // ============================================================
    // FOREIGN KEY: Role
    // ============================================================
    //
    // RoleId points to:
    //
    //      Role.Id
    //
    // This creates:
    //
    //      Role 1 ─────────── * UserRole
    //
    public Guid RoleId { get; set; }

    // Navigation property:
    //
    //      UserRole → Role
    //
    public Role Role { get; set; } = null!;


    // ============================================================
    // ADDITIONAL DATA ABOUT THE RELATIONSHIP
    // ============================================================
    //
    // This is one reason we use UserRole as an entity instead
    // of just letting EF Core create a simple many-to-many table.
    //
    // We can store information about when the role was assigned.
    //
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}