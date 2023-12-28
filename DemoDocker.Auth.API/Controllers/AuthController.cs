using DemoDocker.Library.Data;
using DemoDocker.Library.Modes;

using Microsoft.AspNetCore.Mvc;
using Middleware;

namespace DemoDocker.Auth.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IJwtBuilder _jwtBuilder;
    
    private readonly APIContext _context;

    public AuthController(  APIContext context, IJwtBuilder jwtBuilder )
    {
         _jwtBuilder = jwtBuilder;
         _context = context;
         
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User user, [FromQuery(Name = "d")] string destination = "frontend")
    {
        var u = _context.Users.FirstOrDefault(d=>d.Email == user.Email );

        if (u == null)
        {
            return NotFound("User not found.");
        }

 
        var isValid = u.Password ==user.Password;

        if (!isValid)
        {
            return BadRequest("Could not authenticate user.");
        }

        var token = _jwtBuilder.GetToken(u.Id.ToString(),u.Name,u.Email);

        return Ok(token);
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        var u = _context.Users.FirstOrDefault(d => d.Email == user.Email);

        if (u != null)
        {
            return BadRequest("User already exists.");
        }

        _context.Users.Add(user);
        try
        {
            _context.SaveChanges();
        }
        catch (Exception)
        {

            return BadRequest();
        }
        

        return Ok();
    }

    [HttpGet("validate")]
    public IActionResult Validate([FromQuery(Name = "email")] string email, [FromQuery(Name = "token")] string token)
    {
        var u = _context.Users.FirstOrDefault(d => d.Email == email);
        
        if (u is null)
        {
            return NotFound("User not found.");
        }

        var userId = _jwtBuilder.ValidateToken(token);

        if (userId != u.Id.ToString())
        {
            return BadRequest("Invalid token.");
        }

        return Ok(userId);
    }
}