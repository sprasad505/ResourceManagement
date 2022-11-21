using App.DAL.DataContext;
using App.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace App1.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly UsersContext userContext;
        public AuthController(IConfiguration configuration, UsersContext userContext)
        {
            _configuration = configuration;
            this.userContext = userContext;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(Userdto userdto)
        {
            var data = userContext.Users.Find(userdto.Username);
            if (data != null)
            {
                return BadRequest("User already exists!");
            }
            else
            {
                CreatePasswordHash(userdto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.Username = userdto.Username;
                user.PasswordSalt = passwordSalt;
                user.PasswordHash = passwordHash;
                this.userContext.Add(user);
                this.userContext.SaveChanges();
                return Ok(user);
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(Userdto request)
        {

            var data = userContext.Users.Find(request.Username);
            if (data == null)
            {
                return BadRequest("User doesn't exist!");
            }
            else if (!VerifyPasswordHash(request.Password, data.PasswordHash, data.PasswordSalt))
            {
                return BadRequest("Wrong Password!");
            }
            string token = CreateToken(request);
            return token;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(Userdto user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}

