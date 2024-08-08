namespace PelatihanHari2Api.Models;

public class Course : BaseEntity
{
    public int CourseID { get; set; }
    public string? Title { get; set; }
    public int Credits { get; set; }
    
    public ICollection<Enrollment>? Enrollments { get; set; }
}