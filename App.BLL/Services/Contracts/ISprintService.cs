using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services.Contracts
{
    public interface ISprintService
    {
        public Sprint AddSprints(Sprint s);
        Task<List<Sprint>> GetSprints();
        Task<Sprint> SearchSprint(string name);
        public string PatchSprint(long Id, Sprint s);
        public string DeleteSprint(string Id);
    }
}
