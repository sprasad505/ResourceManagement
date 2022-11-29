using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services.Contracts
{
    public interface IAllocationService
    {
        public Allocation AddAlloc(Allocation a);
        Task<List<Allocation>> GetAllocations();
        Task<Allocation> SearchAllocation(string Id);
        public string PatchAlloc(string Id, Allocation a);
        public string DeleteAllocation(string Id);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);


    }
}
