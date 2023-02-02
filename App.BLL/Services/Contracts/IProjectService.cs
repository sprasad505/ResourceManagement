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
        public string AddProjects(Project p, string id);
        Task<List<Project>> GetProjects();
        Task<List<Project>> SearchProject(string id);
        public string PatchProject(string Id, Project p);
        public string DeleteProject(string Id);

    }
}
