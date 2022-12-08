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
    public class TeamService : ITeamService
    {
        private readonly IGenericRepository<Team> genericRepository;
        public TeamService(IGenericRepository<Team> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public Team AddTeams(Team t)
        {
            return genericRepository.AddTeams(t);
        }

        public string DeleteTeam(string Id)
        {
            var data = genericRepository.DeleteTeam(Id);
            return data;
        }

        public async Task<List<Team>> GetTeams()
        {
            return await this.genericRepository.GetTeams();
        }

        public string PatchTeam(string Id, Team t)
        {
            var data = genericRepository.PatchTeam(Id, t);
            return data;
        }
        public async Task<List<Team>> SearchTeam(string Id)
        {
<<<<<<< HEAD
            var result = await genericRepository.SearchTeam(name);
            return result;
=======
            try
            {
                return await genericRepository.SearchTeam(Id);
            }
            catch
            {
                throw;
            }
>>>>>>> 9c217ad5d6920d22715a57ae8f0e0dc73b33e031
        }
    }
}
