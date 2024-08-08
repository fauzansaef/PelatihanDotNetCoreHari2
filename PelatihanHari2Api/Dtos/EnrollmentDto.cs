using PelatihanHari2Api.Models;

namespace PelatihanHari2Api.Dtos;

public class EnrollmentDto
{
    public int CourseID { get; set; }
    public Grade? Grade { get; set; }
}