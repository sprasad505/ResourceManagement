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

        public string AddSprints(Sprint s)
        {
            return this.genericRepository.AddSprint(s);
        }

        public string DeleteSprint(string Id)
        {
            return this.genericRepository.DeleteSprint(Id);
        }

        public async Task<List<Sprint>> GetSprints()
        {
            return await this.genericRepository.GetSprints();
        }

        public string PatchSprint(long Id, Sprint s)
        {
            return this.genericRepository.PatchSprint(Id, s);
        }

        public async Task<List<Sprint>> SearchSprint(string Id)
        {
            return await this.genericRepository.SearchSprint(Id);
        }
    }
}
