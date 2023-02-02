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
    public class ProjectService : IProjectService
    {
        private readonly IGenericRepository<Project> genericRepository;
        public ProjectService(IGenericRepository<Project> genericRepository)
        {
            this.genericRepository = genericRepository;
        }
        public string AddProjects(Project p, string id)
        {
            return genericRepository.AddProjects(p, id);
        }

        public string DeleteProject(string Id)
        {
            var data = genericRepository.DeleteProject(Id);
            return data;
        }
        public async Task<List<Project>> GetProjects()
        {
            return await this.genericRepository.GetProjects();
        }

        public string PatchProject(string Id, Project p)
        {
            var data = genericRepository.PatchProject(Id, p);
            return data;
        }
        public async Task<List<Project>> SearchProject(string id)
        {
            var result = await genericRepository.SearchProject(id);
            return result;
        }
    }
}
