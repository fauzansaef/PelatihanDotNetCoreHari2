using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PelatihanHari2Api.Dtos;
using PelatihanHari2Api.Models;
using PelatihanHari2Api.Utils;

namespace PelatihanHari2Api.Controllers;
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly SchoolContext _context;
    public CourseController(SchoolContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new MessageResponse("Success", true, _context.Course));
    }
    
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        Console.WriteLine($"Get Course by ID {id}");
        var course = _context.Course.FirstOrDefault(x=>x.CourseID == id);
        if(course == null) 
            return NotFound(new MessageResponse("Failed", false, $"Course, id : {id} not found"));
        
        return Ok(new MessageResponse("Success", true, course));
    }
    
    [HttpGet("title")]
    public IActionResult GetAll([FromQuery] string? title)
    {
        var course = _context.Course.AsQueryable();
        if(title != null)
        {
            course = course.Where(x=>x.Title.ToLower().Contains(title));
        }
        
        if (course.IsNullOrEmpty()) return NotFound(new MessageResponse("Failed", false, "Course not found"));
        
        return Ok(new MessageResponse("Success", true, course));
    }
    
    [HttpPost]
    public IActionResult Post([FromBody] CourseDto course)
    {
        var courseExist = _context.Course.FirstOrDefault(x=>x.Title == course.Title);
        if(courseExist != null) return BadRequest(new MessageResponse("Failed", false, "Course already exist"));
        
        try
        { 
            _context.Course.Add(new Course
            {
                Title = course.Title,
                Credits = course.Credits
            });
            _context.SaveChanges();
            return Ok(new MessageResponse("Success", true, "Course added"));
        }
        catch (Exception e)
        {
            return StatusCode(500,new MessageResponse("Failed", false, e.Message));
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id,[FromBody] CourseDto course)
    {
        var courseData = _context.Course.FirstOrDefault(x=>x.CourseID == id);
        if(courseData == null) return NotFound(new MessageResponse("Failed", false, "Course not found"));
        try
        {
            courseData.Credits = course.Credits;
            courseData.Title = course.Title;
            _context.SaveChanges();
            return Ok(new MessageResponse("Success", true, "Course updated"));
        }
        catch (Exception e)
        {
            return StatusCode(500,new MessageResponse("Failed", false, e.Message));
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var course = _context.Course.FirstOrDefault(x=>x.CourseID == id);
        if(course==null) return NotFound(new MessageResponse("Failed", false, "Course not found"));
        _context.Course.Remove(course);
        _context.SaveChanges();
        return Ok(new MessageResponse("Success",true,"Course deleted"));
    }
}