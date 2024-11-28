using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Immigration.Api.Models;
using Immigration.Api.Data;

namespace Immigration.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        UserData _userData;
       

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration; 
            _userData = new UserData();
        }

        // POST api/login
        [HttpPost]
        public IActionResult Login([FromBody] Login loginRequest)
        {
            // Validate the login (in real apps, you'll use a service to check against DB)
            if (IsValidUser(loginRequest.Username, loginRequest.Password))
            {
                // Create JWT token
                var token = GenerateJwtToken(loginRequest.Username);

                // Return the token
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized("Invalid credentials");
            }
        }

        // Dummy validation for example purposes, replace with your real validation logic
        private bool IsValidUser(string username, string password)
        {
            // Example: replace this logic with your real user authentication         
           return _userData.Users.Any(u => u.UserName == username && u.Password == password);         
        }

        private string GenerateJwtToken(string username)
        {
            // Set JWT token expiration
            var expirationTime = DateTime.UtcNow.AddHours(1);

            // Define claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            // Secret key to sign the JWT token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expirationTime,
                signingCredentials: credentials
            );

            // Return the JWT token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
