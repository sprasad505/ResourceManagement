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
        public Project AddProjects(Project p)
        {
            try
            {
                return genericRepository.AddProjects(p);
            }
            catch
            {
                throw;
            }
        }

        public string DeleteProject(string Id)
        {
            try
            {
               var data= genericRepository.DeleteProject(Id);
                return data;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Project>> GetProjects()
        {
            try
            {
                return await this.genericRepository.GetProjects();
            }
            catch
            {
                throw;
            }
        }

        public string PatchProject(string Id, Project p)
        {
            try
            {
               var data= genericRepository.PatchProject(Id, p);
                return data;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Project> SearchProject(string name)
        {
            try
            {
                var result = await genericRepository.SearchProject(name);
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
