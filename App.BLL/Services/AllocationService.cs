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

        public async Task<List<Allocation>> GetAllocations()
        {
            try
            {
                return await genericRepository.GetAllocations();
            }
            catch
            {
                throw;
            }
        }
        public Allocation AddAlloc(Allocation a)
        {
            try
            {
                Allocation test = null;
                var result = dbContext.Set<Allocation>().ToList();
                foreach (var item in result)
                {
                    if (item.EmployeeId == a.EmployeeId)
                    {
                        test = item;
                        break;
                    }
                }
                    //var data = dbContext.Allocations.Find(a.EmployeeId);
                if (test == null)
                {
                    Resource r = new Resource();
                    //var data1 = dbContext.Resources.Find(a.EmployeeId);
                    var result1 = dbContext.Set<Resource>().ToList();
                    foreach (var item1 in result1)
                    {
                        if (item1.EmployeeId == a.EmployeeId)
                        {
                            r = item1;
                            break;
                        }
                    }
                    Random random = new Random();
                    string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    string password = new string(Enumerable.Repeat(chars, 8)
                        .Select(s => s[random.Next(s.Length)]).ToArray());
                    CreatePasswordHash(password, out byte[] PasswordHash, out byte[] PasswordSalt);
                    genericRepository.Adduser(r.Email,PasswordHash,PasswordSalt );
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
            try
            {
                var data= genericRepository.PatchAllocation(Id, a);
                return data;

            }
            catch
            {
                throw;
            }
        }

        public string DeleteAllocation(string Id)
        {
            try
            {
                var data= genericRepository.DeleteAllocation(Id);
                return data;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Allocation> SearchAllocation(string Id)
        {
            try
            {
                var result = await genericRepository.SearchAllocation(Id);
                return result;
            }
            catch
            {
                throw;
            }
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
