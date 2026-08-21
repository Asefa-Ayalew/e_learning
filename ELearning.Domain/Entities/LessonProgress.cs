namespace ELearning.Domain.Entities;

public class LessonProgress
{
    public Guid Id { get; set; }


    // ============================================================
    // FOREIGN KEY: User
    // ============================================================
    //
    // User 1 ─────────── * LessonProgress
    //
    // One User can have MANY progress records.
    //
    // UserId → User.Id
    //
    public Guid UserId { get; set; }

    // Navigation:
    //
    //      LessonProgress → User
    //
    public User User { get; set; } = null!;


    // ============================================================
    // FOREIGN KEY: Lesson
    // ============================================================
    //
    // Lesson 1 ─────────── * LessonProgress
    //
    // One Lesson can have MANY progress records.
    //
    // LessonId → Lesson.Id
    //
    public Guid LessonId { get; set; }

    // Navigation:
    //
    //      LessonProgress → Lesson
    //
    public Lesson Lesson { get; set; } = null!;


    // ============================================================
    // MANY-TO-MANY CONCEPT
    // ============================================================
    //
    // From the business perspective:
    //
    //      User * ─────────── * Lesson
    //
    // One User can have progress on MANY Lessons.
    //
    // One Lesson can have progress records for MANY Users.
    //
    // Therefore:
    //
    //      User ↔ Lesson = MANY-TO-MANY
    //
    // LessonProgress is the JOIN ENTITY.
    //
    // It also stores additional information about the relationship:
    //
    //      IsCompleted
    //      WatchedSeconds
    //      CompletedAt
    //      LastAccessedAt
    //
    public bool IsCompleted { get; set; }

    public int WatchedSeconds { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime LastAccessedAt { get; set; } = DateTime.UtcNow;
}