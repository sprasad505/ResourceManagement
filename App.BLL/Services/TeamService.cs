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
            try
            {
                return genericRepository.AddTeams(t);
            }
            catch
            {
                throw;
            }
        }

        public string DeleteTeam(string Id)
        {
            try
            {
                var data= genericRepository.DeleteTeam(Id);
                return data;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Team>> GetTeams()
        {
            try
            {
                return await this.genericRepository.GetTeams();
            }
            catch
            {
                throw;
            }
        }

        public string PatchTeam(string Id, Team t)
        {
            try
            {
                var data= genericRepository.PatchTeam(Id, t);
                return data;

            }
            catch
            {
                throw;
            }
        }
        public async Task<Team> SearchTeam(string name)
        {
            try
            {
                var result = await genericRepository.SearchTeam(name);
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
