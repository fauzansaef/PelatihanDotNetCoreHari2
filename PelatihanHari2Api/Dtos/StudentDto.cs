namespace PelatihanHari2Api.Dtos;

public class StudentDto
{
    public string FirstMidName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? EnrollmentDate { get; set; }
}