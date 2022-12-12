using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services.Contracts
{
    public interface ITeamService
    {
        public string AddTeams(Team t);
        Task<List<Team>> GetTeams();
        Task<List<Team>> SearchTeam(string Id);
        public string PatchTeam(string Id, Team t);
        public string DeleteTeam(string Id);
        
    }
}
