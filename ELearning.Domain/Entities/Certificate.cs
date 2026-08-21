namespace ELearning.Domain.Entities;

public class Certificate
{
    public Guid Id { get; set; }


    // ============================================================
    // FOREIGN KEY: User
    // ============================================================
    //
    // User 1 ─────────── * Certificate
    //
    // One User can earn MANY certificates.
    //
    // UserId → User.Id
    //
    public Guid UserId { get; set; }

    // Navigation:
    //
    //      Certificate → User
    //
    public User User { get; set; } = null!;


    // ============================================================
    // FOREIGN KEY: Course
    // ============================================================
    //
    // Course 1 ─────────── * Certificate
    //
    // One Course can produce MANY certificates.
    //
    // Example:
    //
    //      ASP.NET Course
    //          ├── Certificate → John
    //          ├── Certificate → Sarah
    //          └── Certificate → David
    //
    // CourseId → Course.Id
    //
    public Guid CourseId { get; set; }

    // Navigation:
    //
    //      Certificate → Course
    //
    public Course Course { get; set; } = null!;


    // ============================================================
    // MANY-TO-MANY CONCEPT
    // ============================================================
    //
    // From the business perspective:
    //
    //      User * ─────────── * Course
    //
    // A User can earn certificates for MANY Courses.
    //
    // A Course can have certificates for MANY Users.
    //
    // Certificate represents the relationship between them.
    //
    public string CertificateNumber { get; set; } = null!;

    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

    public string? CertificateUrl { get; set; }
}