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
        public string AddAlloc(Allocation a);
        Task<List<Alloc>> GetAllocations();
        Task<List<Alloc>> SearchAllocation(string Id);
        public string PatchAlloc(string Id, Allocation a);
        public string DeleteAllocation(string Id);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public string GetTeams(string projId, string resId);

    }
}
