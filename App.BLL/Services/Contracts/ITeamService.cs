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
        public Team AddTeams(Team t);
        Task<List<Team>> GetTeams();
        Task<Team> SearchTeam(string name);
        public string PatchTeam(string Id, Team t);
        public string DeleteTeam(string Id);
        
    }
}
