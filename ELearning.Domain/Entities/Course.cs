namespace ELearning.Domain.Entities;

public class Course
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    public string? ThumbnailUrl { get; set; }

    public decimal Price { get; set; }

    public bool IsFree { get; set; }

    public bool IsPublished { get; set; }

    public DateTime? PublishedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }


    // ============================================================
    // MANY-TO-ONE / ONE-TO-MANY: Course → Instructor
    // ============================================================
    //
    // Relationship:
    //
    //      User 1  ───────────  * Course
    //
    // Meaning:
    //      One User can be the instructor of MANY Courses.
    //
    // Example:
    //
    //      User: John
    //          │
    //          ├── C# Fundamentals
    //          ├── ASP.NET Core
    //          └── EF Core
    //
    // Course is the MANY side.
    //
    // Therefore, the foreign key is stored in Course:
    //
    //      InstructorId
    //
    // InstructorId points to:
    //
    //      User.Id
    //
    public Guid InstructorId { get; set; }

    // Navigation property.
    //
    // This allows us to navigate:
    //
    //      Course → Instructor/User
    //
    // InstructorId is the actual FK value.
    // Instructor is the object/navigation property.
    //
    public User Instructor { get; set; } = null!;


    // ============================================================
    // MANY-TO-ONE / ONE-TO-MANY: Course → Category
    // ============================================================
    //
    // Relationship:
    //
    //      Category 1  ───────────  * Course
    //
    // Meaning:
    //      One Category can contain MANY Courses.
    //
    // But each Course belongs to ONE Category.
    //
    // Example:
    //
    //      Programming
    //          ├── C#
    //          ├── ASP.NET
    //          └── EF Core
    //
    // CategoryId is the FOREIGN KEY.
    //
    // It points to:
    //
    //      Category.Id
    //
    public Guid CategoryId { get; set; }

    // Navigation property.
    //
    // Allows:
    //
    //      Course → Category
    //
    // CategoryId = actual FK value
    // Category   = navigation property
    //
    public Category Category { get; set; } = null!;


    // ============================================================
    // ONE-TO-MANY: Course → Sections
    // ============================================================
    //
    // Relationship:
    //
    //      Course 1  ───────────  * Section
    //
    // One Course can contain MANY Sections.
    //
    // Example:
    //
    //      ASP.NET Core Course
    //          │
    //          ├── Introduction
    //          ├── Controllers
    //          ├── EF Core
    //          └── Authentication
    //
    // The foreign key is stored in Section:
    //
    //      Section.CourseId
    //
    // because Section is the MANY side.
    //
    public ICollection<Section> Sections { get; set; }
        = new List<Section>();


    // ============================================================
    // ONE-TO-MANY: Course → Enrollments
    // ============================================================
    //
    // One Course can have MANY Enrollment records.
    //
    // Example:
    //
    //      ASP.NET Core Course
    //          │
    //          ├── Enrollment → John
    //          ├── Enrollment → Sarah
    //          └── Enrollment → David
    //
    // The foreign key is stored in Enrollment:
    //
    //      Enrollment.CourseId
    //
    public ICollection<Enrollment> Enrollments { get; set; }
        = new List<Enrollment>();


    // ============================================================
    // ONE-TO-MANY: Course → CourseReviews
    // ============================================================
    //
    // One Course can have MANY reviews.
    //
    // Example:
    //
    //      ASP.NET Core
    //          │
    //          ├── Review → John → 5 stars
    //          ├── Review → Sarah → 4 stars
    //          └── Review → David → 5 stars
    //
    // The foreign key is stored in CourseReview:
    //
    //      CourseReview.CourseId
    //
    public ICollection<CourseReview> Reviews { get; set; }
        = new List<CourseReview>();


    // ============================================================
    // ONE-TO-MANY: Course → Certificates
    // ============================================================
    //
    // One Course can have MANY certificates.
    //
    // Example:
    //
    //      ASP.NET Core Course
    //          │
    //          ├── Certificate → John
    //          ├── Certificate → Sarah
    //          └── Certificate → David
    //
    // The foreign key is stored in Certificate:
    //
    //      Certificate.CourseId
    //
    public ICollection<Certificate> Certificates { get; set; }
        = new List<Certificate>();
}