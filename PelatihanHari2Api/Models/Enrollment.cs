using System.Text.Json.Serialization;

namespace PelatihanHari2Api.Models;

public class Enrollment : BaseEntity
{
 
    public int EnrollmentID { get; set; }
    public int CourseID { get; set; }
    public int StudentID { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Grade? Grade { get; set; }
    
    [JsonIgnore] 
    public Student? Student { get; set; }
    
    [JsonIgnore] 
    public Course? Course { get; set; }
}

public enum Grade
{
    A, B, C, D, E, F

}