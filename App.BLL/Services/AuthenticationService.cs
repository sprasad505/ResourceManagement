using App.BLL.Services.Contracts;
using App.DAL.DataContext;
using App.DAL.Middlewares;
using App.DAL.Models;
using App.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
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
        private readonly ResourcedbContext resourcedb;
        public AuthenticationService(IGenericRepository<User> genericRepository, IConfiguration configuration,ResourcedbContext resourcedb)
        {
            this.genericRepository = genericRepository;
            this.configuration = configuration;
            this.resourcedb = resourcedb;
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
            Resource r = new Resource();
            var result1 = resourcedb.Set<Resource>().ToList();
            foreach (var item1 in result1)
            {
                if (item1.Email == user.Username)
                {
                    r = item1;
                    break;
                }
            }
            List<Claim> claims = new List<Claim>
             {
                 new Claim("Name", r.Name) ,
                 new Claim("UserName", user.Username),
                 new Claim("Id", r.Id.ToString()),
                 new Claim("role", ((int?)r.Role).ToString())
             };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
               // expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public string Login(Userdto request)
        {
            try
            {
                var data = genericRepository.Login(request);
                if (data == null)
                {
                    return "Wrong Credentials!";
                }
                else if (!VerifyPasswordHash(request.Password, data.PasswordHash, data.PasswordSalt))
                {
                    throw new APIException(409, "Wrong Credentials");
                }
                string token = CreateToken(request);
                return token;
            }
            catch (Exception e)
            {
                throw e;
            }
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
