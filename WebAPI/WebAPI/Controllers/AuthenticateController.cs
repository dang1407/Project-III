using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Application;
using System.Security.Cryptography;
using WebAPI.Domain;
using OfficeOpenXml.Drawing;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase  
    {
        private readonly IUserService _userService;
        public IConfiguration _configuration;
        public AuthenticateController(IUserService userService, IConfiguration configuration) 
        {
            _userService = userService; 
            _configuration = configuration; 
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginDTO loginDTO)
        {
            if (string.IsNullOrEmpty(loginDTO.UserName) || string.IsNullOrEmpty(loginDTO.Password))
            {
                return BadRequest();
            }

            var user = await _userService.FindAccountAsync(loginDTO);

            string hashedPassword = ComputeSha256Hash(loginDTO.Password);   

            if (user != null && user.Password.CompareTo(hashedPassword) == 0 )
            {
                ////create claims details based on the user information
                //var claims = new[] {
                //        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                //        //new Claim("UserId", user.AccountId.ToString()),
                //        //new Claim("DisplayName", user.DisplayName),
                //        new Claim("UserName", user.UserName),
                //        new Claim(ClaimTypes.Role, user.Role)
                //    };
                //Console.WriteLine(_configuration["Jwt:Issuer"].ToString());
                //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                //var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //var token = new JwtSecurityToken(
                //    _configuration["Jwt:Issuer"],
                //    _configuration["Jwt:Audience"],         
                //    claims,
                //    expires: DateTime.UtcNow.AddDays(3),
                //    signingCredentials: signIn);

                //return Ok(new JwtSecurityTokenHandler().WriteToken(token));

                var issuer = _configuration["Jwt:Issuer"].ToString();
                var audience = _configuration["Jwt:Audience"].ToString();
                var key = Encoding.ASCII.GetBytes
                (_configuration["Jwt:Key"].ToString());
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()
                ),
                new Claim(ClaimTypes.Role, user.Role)
             }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);
                return Ok(new {
                    AccessToken = stringToken,
                    Role = user.Role,
            });
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("relogin")]
        public IActionResult ReLogin()
        {
            var roles = User.FindAll(ClaimTypes.Role)?.Select(c => c.Value);

            return Ok(roles);
        }
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
