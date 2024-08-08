using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PelatihanHari2Api.Dtos;
using PelatihanHari2Api.Models;
using PelatihanHari2Api.Utils;

namespace PelatihanHari2Api.Controllers;
[Route("api/[controller]")]
public class StudentController(SchoolContext context, IMapper mapper) : ControllerBase
{ 
    
    [HttpGet]
   public IActionResult Get()
   {
       return Ok(new MessageResponse("Success", true, context.Students));
   }
   
   [HttpGet("{id}")]
   public IActionResult Get(int id)
   {
       Console.WriteLine($"Get Student by ID {id}");
       var student = context.Students.Where(x=>x.ID == id)
           .Include(x=>x.Enrollments)!
           .ThenInclude(x=>x.Course)
           .FirstOrDefault();
       if(student == null) 
           return NotFound(new MessageResponse("Failed", false, $"Student, id : {id} not found"));
       
       return Ok(new MessageResponse("Success", true, student));
   }

   [HttpGet("name")]
   public IActionResult GetAll([FromQuery] string? name)
   {
       var student = context.Students.AsQueryable();
       if(name != null)
       {
           student = student.Where(x=>x.FirstMidName.ToLower().Contains(name) || x.LastName.ToLower().Contains(name));
       }

       if (student.IsNullOrEmpty()) return NotFound(new MessageResponse("Failed", false, "Student not found"));
       
       return Ok(new MessageResponse("Success", true, student));
   }
   
   [HttpPost]
   public IActionResult Post([FromBody] StudentDto student)
   {
       var studentExist = context.Students.FirstOrDefault(x=>x.FirstMidName == student.FirstMidName && x.LastName == student.LastName);
         if(studentExist != null) return BadRequest(new MessageResponse("Failed", false, "Student already exist"));
         
       try
       {
           var studentData = mapper.Map<Student>(student);
           context.Students.Add(studentData);
           context.SaveChanges();
           return Ok(new MessageResponse("Success", true, student));
       }catch(Exception e)
       {
           return StatusCode(500,new MessageResponse("Failed", false, e.Message)); 
       }
       
   }
   
   [HttpPut("{id}")]
   public IActionResult Update(int id, [FromBody] StudentDto student)
   {
       var studentData = context.Students.FirstOrDefault(x=>x.ID == id);
       if(studentData == null) return NotFound(new MessageResponse("Failed", false, $"Student, id : {id} not found"));
       try
       {
           studentData.FirstMidName = student.FirstMidName;
           studentData.LastName = student.LastName;
           studentData.EnrollmentDate = student.EnrollmentDate;
           studentData.UpdatedAt = DateTime.Now;
           context.SaveChanges();
           return Ok(new MessageResponse("Success", true, studentData));
       }catch(Exception e)
       {
           return StatusCode(500,new MessageResponse("Failed", false, e.Message)); 
       }
       
      
   }

   [HttpDelete("{id}")]
   public IActionResult Delete(int id)
   {
       var student = context.Students.FirstOrDefault(x=>x.ID == id);
       if (student == null) return NotFound(new MessageResponse("Failed", false, $"Student, id : {id} not found"));
       context.Students.Remove(student);
       context.SaveChanges();
       return Ok(new MessageResponse("Success", true, student));
   }
   
   [HttpPost("{id}/Enrollment")]
   public IActionResult AddEnrollment(int id, [FromBody] EnrollmentDto enrollment)
   {
       var student = context.Students.FirstOrDefault(x=>x.ID == id);
       if(student == null) return NotFound(new MessageResponse("Failed", false, $"Student, id : {id} not found"));
       
       var course = context.Course.FirstOrDefault(x=>x.CourseID == enrollment.CourseID);
       if(course == null) return NotFound(new MessageResponse("Failed", false, $"Course, id : {enrollment.CourseID} not found"));
       
       var enrollmentExist = context.Enrollment.FirstOrDefault(x=>x.StudentID == id && x.CourseID == enrollment.CourseID);
       if(enrollmentExist != null) return BadRequest(new MessageResponse("Failed", false, "Enrollment already exist"));
       
       try
       { 
           var enrollmentData = mapper.Map<Enrollment>(enrollment);
           enrollmentData.StudentID = id;
           context.Enrollment.Add(enrollmentData);
           context.SaveChanges();
           return Ok(new MessageResponse("Success", true, enrollment));
           
       }catch(Exception e)
       {
           return StatusCode(500,new MessageResponse("Failed", false, e.Message)); 
       }
   }
   
}