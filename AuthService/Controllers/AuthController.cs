using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthService.Data;
using AuthService.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using AuthService.LoginModel;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthServiceContext _context;
        private readonly IConfiguration _config;

        public AuthController(AuthServiceContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel.LoginModel login)
        {
            var student = _context.Student.FirstOrDefault(s => s.Username == login.Username && s.Password == login.Password);
            if (student == null)
            {
                return Unauthorized();
            }
            else
            {
                var token = Generate(student);
                return Ok(token);
            }
        }
        
        private string Generate(Student student)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, student.Username),
                new Claim(ClaimTypes.Email, student.Email)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
