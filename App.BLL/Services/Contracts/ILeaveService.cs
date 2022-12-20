using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services.Contracts
{
    public interface ILeaveService
    {
        Task<List<Leave>> GetLeaves(string EmployeeId);
        Task<List<Leave>> GetallLeaves();
        public string AddLeave(Leave l);
        public string PatchLeave(string Id, Leave l);
        public string DeleteLeave(string Id);

    }
}
