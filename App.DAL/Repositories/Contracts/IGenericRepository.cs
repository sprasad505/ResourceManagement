using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using App.DAL.Models;

namespace App.DAL.Repositories.Contracts
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<List<Allocation>> GetAllocations();
        Task<List<Team>> GetTeams();
        Task<List<Project>> GetProjects();
        Task<List<Resource>> GetResources();
        Task<List<Sprint>> GetSprints();
        Task<Allocation>SearchAllocation(string Id);
        Task<Resource> SearchResource(string Id);
        Task<Team> SearchTeam(string name);
        Task<Project> SearchProject(string name);
        Task<Sprint> SearchSprint(string name);
        public Resource AddResources(Resource r);
        public Project AddProjects(Project p);
        public Team AddTeams(Team t);
        public Allocation AddAlloc(Allocation a);
        public Sprint AddSprint(Sprint s);
        public void Adduser(string email, byte[] passwordHash, byte[] passwordSalt);
        public User Login(Userdto request);
        public string PatchAllocation(string Id, Allocation allocation);
        public string PatchTeam(string Id, Team team);
        public string PatchProject(string Id, Project project);
        public string PatchResource(string Id, Resource resource);
        public string PatchSprint(string Id, Sprint sprint);
        public string DeleteAllocation(string Id);
        public string DeleteTeam(string Id);
        public string DeleteProject(string Id);
        public string DeleteResource(string Id);
        public string DeleteSprint(string Id);
    }
}
