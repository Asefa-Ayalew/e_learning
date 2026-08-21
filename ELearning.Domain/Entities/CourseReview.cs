namespace ELearning.Domain.Entities;

public class CourseReview
{
    public Guid Id { get; set; }


    // ============================================================
    // FOREIGN KEY: Course
    // ============================================================
    //
    // Course 1 ─────────── * CourseReview
    //
    // One Course can have MANY reviews.
    //
    // CourseId → Course.Id
    //
    public Guid CourseId { get; set; }

    // Navigation:
    //
    //      CourseReview → Course
    //
    public Course Course { get; set; } = null!;


    // ============================================================
    // FOREIGN KEY: User
    // ============================================================
    //
    // User 1 ─────────── * CourseReview
    //
    // One User can write MANY reviews.
    //
    // UserId → User.Id
    //
    public Guid UserId { get; set; }

    // Navigation:
    //
    //      CourseReview → User
    //
    public User User { get; set; } = null!;


    // ============================================================
    // MANY-TO-MANY CONCEPT
    // ============================================================
    //
    // From the business perspective:
    //
    //      User * ─────────── * Course
    //
    // One User can review MANY Courses.
    //
    // One Course can be reviewed by MANY Users.
    //
    // CourseReview acts as the JOIN/ASSOCIATIVE ENTITY.
    //
    // It contains additional relationship data:
    //
    //      Rating
    //      Comment
    //      IsPublished
    //      CreatedAt
    //
    public int Rating { get; set; }

    public string? Comment { get; set; }

    public bool IsPublished { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}