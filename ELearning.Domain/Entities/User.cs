namespace ELearning.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? ProfileImageUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsEmailVerified { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }


    // ============================================================
    // MANY-TO-MANY: User ↔ Role
    // ============================================================
    //
    // Conceptually:
    //
    //      User  *  ───────────  *  Role
    //
    // A User can have MANY Roles.
    //
    // Example:
    //
    //      John
    //        ├── Student
    //        └── Instructor
    //
    // A Role can also belong to MANY Users.
    //
    // Example:
    //
    //      Student
    //        ├── John
    //        ├── Sarah
    //        └── David
    //
    // Therefore this is MANY-TO-MANY.
    //
    // We don't connect User directly to Role.
    //
    // We use an intermediate/junction entity:
    //
    //      UserRole
    //
    // So the actual relationships are:
    //
    //      User 1 ─── * UserRole
    //
    //      Role 1 ─── * UserRole
    //
    public ICollection<UserRole> UserRoles { get; set; }
        = new List<UserRole>();


    // ============================================================
    // ONE-TO-MANY: User → Courses
    // ============================================================
    //
    // This represents courses where this User is the instructor.
    //
    //      User 1 ─────────── * Course
    //
    // One instructor can teach MANY courses.
    //
    // The foreign key is:
    //
    //      Course.InstructorId
    //
    // which points to:
    //
    //      User.Id
    //
    public ICollection<Course> Courses { get; set; }
        = new List<Course>();


    // ============================================================
    // ONE-TO-MANY: User → Enrollments
    // ============================================================
    //
    // One User can have MANY Enrollment records.
    //
    // Example:
    //
    //      John
    //        ├── Enrollment → C# Course
    //        ├── Enrollment → ASP.NET
    //        └── Enrollment → EF Core
    //
    // The foreign key is:
    //
    //      Enrollment.UserId
    //
    public ICollection<Enrollment> Enrollments { get; set; }
        = new List<Enrollment>();


    // ============================================================
    // ONE-TO-MANY: User → RefreshTokens
    // ============================================================
    //
    // One User can have MANY refresh tokens.
    //
    // Example:
    //
    //      John
    //        ├── Laptop refresh token
    //        ├── Phone refresh token
    //        └── Tablet refresh token
    //
    // The foreign key is:
    //
    //      RefreshToken.UserId
    //
    public ICollection<RefreshToken> RefreshTokens { get; set; }
        = new List<RefreshToken>();


    // ============================================================
    // ONE-TO-MANY: User → CourseReviews
    // ============================================================
    //
    // One User can write MANY reviews.
    //
    // Example:
    //
    //      John
    //        ├── Review → C# Course
    //        ├── Review → ASP.NET Course
    //        └── Review → EF Core Course
    //
    // The foreign key is:
    //
    //      CourseReview.UserId
    //
    public ICollection<CourseReview> CourseReviews { get; set; }
        = new List<CourseReview>();


    // ============================================================
    // ONE-TO-MANY: User → LessonProgress
    // ============================================================
    //
    // One User can have MANY lesson-progress records.
    //
    // Example:
    //
    //      John
    //        ├── Lesson 1 → Completed
    //        ├── Lesson 2 → 50%
    //        └── Lesson 3 → Completed
    //
    // The foreign key is:
    //
    //      LessonProgress.UserId
    //
    public ICollection<LessonProgress> LessonProgresses { get; set; }
        = new List<LessonProgress>();


    // ============================================================
    // ONE-TO-MANY: User → Certificates
    // ============================================================
    //
    // One User can earn MANY certificates.
    //
    // Example:
    //
    //      John
    //        ├── Certificate → C#
    //        ├── Certificate → ASP.NET
    //        └── Certificate → EF Core
    //
    // The foreign key is:
    //
    //      Certificate.UserId
    //
    public ICollection<Certificate> Certificates { get; set; }
        = new List<Certificate>();
}