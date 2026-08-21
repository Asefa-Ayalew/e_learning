using ELearning.Domain.Enums;

namespace ELearning.Domain.Entities;

public class Lesson
{
    public Guid Id { get; set; }


    // ============================================================
    // FOREIGN KEY: Section
    // ============================================================
    //
    // Relationship:
    //
    //      Section 1 ─────────── * Lesson
    //
    // One Section can contain MANY Lessons.
    //
    // Lesson is the MANY side.
    //
    // Therefore the foreign key is here:
    //
    //      SectionId
    //
    // SectionId → Section.Id
    //
    public Guid SectionId { get; set; }


    // Navigation property:
    //
    //      Lesson → Section
    //
    public Section Section { get; set; } = null!;


    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public LessonType Type { get; set; }

    public string? VideoUrl { get; set; }

    public string? Content { get; set; }

    public int DurationInSeconds { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsFreePreview { get; set; }

    public bool IsPublished { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }


    // ============================================================
    // ONE-TO-MANY: Lesson → LessonProgress
    // ============================================================
    //
    // One Lesson can have MANY progress records.
    //
    // Why?
    //
    //      Lesson: "Introduction to C#"
    //
    //      ├── Progress → John
    //      ├── Progress → Sarah
    //      └── Progress → David
    //
    // So:
    //
    //      Lesson 1 ─────────── * LessonProgress
    //
    // The foreign key is:
    //
    //      LessonProgress.LessonId
    //
    public ICollection<LessonProgress> ProgressRecords { get; set; }
        = new List<LessonProgress>();
}