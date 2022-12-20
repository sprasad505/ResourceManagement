using App.BLL.Services.Contracts;
using App.DAL.Models;
using App.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly IGenericRepository<Leave> genericRepository;
        public LeaveService(IGenericRepository<Leave> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public string AddLeave(Leave l)
        {
            return this.genericRepository.AddLeave(l);
        }

        public string DeleteLeave(string Id)
        {
            return this.genericRepository.DeleteLeave(Id);
        }

        public async Task<List<Leave>> GetLeaves(string EmployeeId)
        {
            return await this.genericRepository.GetLeaves(EmployeeId);
        }
        public async Task<List<Leave>> GetallLeaves()
        {
            return await this.genericRepository.GetallLeaves();
        }

        public string PatchLeave(string Id, Leave l)
        {
            return this.genericRepository.PatchLeave(Id, l);
        }
    }
}
