namespace ELearning.Domain.Entities;

public class Role
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }


    // ============================================================
    // ONE-TO-MANY: Role → UserRoles
    // ============================================================
    //
    // Remember:
    //
    // Conceptually User ↔ Role is MANY-TO-MANY.
    //
    // But we represent it using UserRole:
    //
    //      User 1 ─── * UserRole * ─── 1 Role
    //
    // One Role can have MANY UserRole records.
    //
    // Example:
    //
    //      Role: Student
    //          │
    //          ├── UserRole → John
    //          ├── UserRole → Sarah
    //          └── UserRole → David
    //
    // The foreign key is:
    //
    //      UserRole.RoleId
    //
    public ICollection<UserRole> UserRoles { get; set; }
        = new List<UserRole>();
}