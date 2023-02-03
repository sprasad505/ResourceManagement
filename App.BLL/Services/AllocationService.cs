using App.BLL.Services.Contracts;
using App.DAL.DataContext;
using App.DAL.Models;
using App.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class AllocationService : IAllocationService
    {
        private readonly IGenericRepository<Allocation> genericRepository;
        private readonly ResourcedbContext dbContext;
        public AllocationService(IGenericRepository<Allocation> genericRepository,ResourcedbContext dbcontext)
        {
            this.genericRepository = genericRepository;
            this.dbContext = dbcontext;
        }

        public async Task<List<Alloc>> GetAllocations()
        {
            return await genericRepository.GetAllocations();
        }
        public string AddAlloc(Allocation a)
        {
            try
            {
                Allocation test = null;
                var result = dbContext.Set<Allocation>().ToList();
                foreach (var item in result)
                {
                    if (item.ResourceId == a.ResourceId)
                    {
                        test = item;
                        break;
                    }
                }
                if (test == null)
                {
                    var resource = dbContext.Resources.Find(a.ResourceId);
                    /*Random random = new Random();
                    string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    string password = new string(Enumerable.Repeat(chars, 8)
                        .Select(s => s[random.Next(s.Length)]).ToArray());*/
                    string password = resource.Name + "@123";
                    CreatePasswordHash(password, out byte[] PasswordHash, out byte[] PasswordSalt);
                    Console.WriteLine("password : " + password);
                    genericRepository.Adduser(resource.Email, PasswordHash, PasswordSalt);
                    return genericRepository.AddAlloc(a);
                }
                else
                {
                    return genericRepository.AddAlloc(a);
                }
            }
            catch
            {
                throw;
            }
        }
        public string PatchAlloc(string Id, Allocation a)
        {
            var data = genericRepository.PatchAllocation(Id, a);
            return data;
        }

        public string DeleteAllocation(string Id)
        {
            var data = genericRepository.DeleteAllocation(Id);
            return data;
        }

        public async Task<List<Alloc>> SearchAllocation(string Id)
        {
            var result = await genericRepository.SearchAllocation(Id);
            return result;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
