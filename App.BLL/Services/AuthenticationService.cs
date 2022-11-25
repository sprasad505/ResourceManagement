using App.BLL.Services.Contracts;
using App.DAL.Models;
using App.DAL.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository<User> genericRepository;
        private readonly IConfiguration configuration;
        public AuthenticationService(IGenericRepository<User> genericRepository, IConfiguration configuration)
        {
            this.genericRepository = genericRepository;
            this.configuration = configuration;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public string CreateToken(Userdto user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public string Login(Userdto request)
        {
            var data=genericRepository.Login(request);
            if (data == null)
            {
                return "User doesn't exist!";
            }
            else if (!VerifyPasswordHash(request.Password, data.PasswordHash, data.PasswordSalt))
            {
                return "Wrong Password!";
            }
            string token = CreateToken(request);
            return token;
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
