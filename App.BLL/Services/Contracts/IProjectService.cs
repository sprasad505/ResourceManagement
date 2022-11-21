using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services.Contracts
{
    public interface IProjectService
    {
        public Project AddProjects(Project p);
        Task<List<Project>> GetProjects();
        Task<Project> SearchProject(string name);
        public string PatchProject(string Id, Project p);
        public string DeleteProject(string Id);

    }
}
