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
    public class SprintService : ISprintService
    {
        private readonly IGenericRepository<Sprint> genericRepository;
        public SprintService(IGenericRepository<Sprint> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public Sprint AddSprints(Sprint s)
        {
            try
            {
                return this.genericRepository.AddSprint(s);
            }
            catch
            {
                throw;
            }
        }

        public string DeleteSprint(string Id)
        {
            try
            {
                return this.genericRepository.DeleteSprint(Id);  
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Sprint>> GetSprints()
        {
            try
            {
                return await this.genericRepository.GetSprints();
            }
            catch
            {
                throw;
            }
        }

        public string PatchSprint(long Id, Sprint s)
        {
            try
            {
                return this.genericRepository.PatchSprint(Id, s);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Sprint>> SearchSprint(string Id)
        {
            try
            {
                return await this.genericRepository.SearchSprint(Id);
            }
            catch
            {
                throw;
            }
        }
    }
}
