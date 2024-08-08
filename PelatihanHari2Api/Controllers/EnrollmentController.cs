using Microsoft.AspNetCore.Mvc;
using PelatihanHari2Api.Dtos;
using PelatihanHari2Api.Models;
using PelatihanHari2Api.Utils;

namespace PelatihanHari2Api.Controllers;

[Route("api/[controller]")]
public class EnrollmentController : ControllerBase
{
    private readonly SchoolContext _context;
    public EnrollmentController(SchoolContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new MessageResponse("Success", true, _context.Enrollment));
    }
}