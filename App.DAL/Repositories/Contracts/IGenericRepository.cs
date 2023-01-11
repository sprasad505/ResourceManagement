using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using App.DAL.Models;
using Microsoft.VisualBasic;

namespace App.DAL.Repositories.Contracts
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<List<Alloc>> GetAllocations();
        Task<List<Team>> GetTeams();
        Task<List<Leave>> GetallLeaves();
        Task<List<Project>> GetProjects();
        Task<List<Resource>> GetResources();
        Task<List<Sprint>> GetSprints();
        Task<List<Story>> GetStories();
        Task<List<Point>> GetPoints();
        Task<List<Leave>> GetLeaves(string Id);
        Task<List<Alloc>>SearchAllocation(string Id);
        Task<List<Resource>> SearchResource(string Id);
        Task<List<Team>> SearchTeam(string Id);
        Task<Project> SearchProject(string name);
        Task<List<Sprint>> SearchSprint(string Id);
        Task<List<Story>> SearchStory(string Id);
        Task<List<Story>> StoriesLeft(string Id);
        Task<List<Resource>> GetScrumMaster();
        Task<List<Story>> GetAddedStories(string Id);
        public string AddLeave(Leave l);
        public string AddResources(Resource r);
        public string AddProjects(Project p);
        public string AddTeams(Team t);
        public string AddAlloc(Allocation a);
        public string AddScrumMaster(Resource r);
        public string AddSprint(Sprint s);
        public string AddStory(Story st);
        public string AddPoint(Point po);
        public void Adduser(string email, byte[] passwordHash, byte[] passwordSalt);
        public User Login(Userdto request);
        public string AddHolidays(InterCalender c);
        Task<List<Calendar22>> GetHolidays();
        public string PatchAllocation(string Id, Allocation allocation);
        public string PatchTeam(string Id, Team team);
        public string PatchLeave(string Id, Leave l);
        public string PatchProject(string Id, Project project);
        public string PatchResource(string Id, Resource resource);
        public string PatchSprint(long Id, Sprint sprint);
        public string PatchStory(string Id, Story st);
        public string PatchPoint(String Id,Point po);
        public string AddStorytoSprint(List<Story> st);
        public string PlanningSprint(string Id);
        public string DeleteAllocation(string Id);
        public string DeleteTeam(string Id);
        public string DeleteProject(string Id);
        public string DeleteResource(string Id);
        public string DeleteSprint(string Id);
        public string DeleteLeave(string Id);
        Task<Story> DeleteStory(string Id);
        public string DeletePoint(string Id);
    }
}
