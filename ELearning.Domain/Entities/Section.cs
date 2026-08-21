namespace ELearning.Domain.Entities;

public class Section
{
    public Guid Id { get; set; }


    // ============================================================
    // FOREIGN KEY: Course
    // ============================================================
    //
    // Relationship:
    //
    //      Course 1 ─────────── * Section
    //
    // One Course has MANY Sections.
    //
    // This foreign key belongs on Section because Section
    // is the MANY side.
    //
    // Section.CourseId → Course.Id
    //
    public Guid CourseId { get; set; }


    // Navigation property:
    //
    //      Section → Course
    //
    public Course Course { get; set; } = null!;


    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }


    // ============================================================
    // ONE-TO-MANY: Section → Lessons
    // ============================================================
    //
    // One Section can have MANY Lessons.
    //
    //      Section 1 ─────────── * Lesson
    //
    // The foreign key is stored in Lesson:
    //
    //      Lesson.SectionId
    //
    public ICollection<Lesson> Lessons { get; set; }
        = new List<Lesson>();
}