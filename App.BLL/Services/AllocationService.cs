using App.BLL.Services.Contracts;
using App.DAL.Models;
using App.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class AllocationService : IAllocationService
    {
        private readonly IGenericRepository<Allocation> genericRepository;
        public AllocationService(IGenericRepository<Allocation> genericRepository)
        {
            this.genericRepository = genericRepository;
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
                return genericRepository.AddAlloc(a);
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
                return genericRepository.PatchAllocation(Id, a);

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
                return genericRepository.DeleteAllocation(Id);
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
    }
}
