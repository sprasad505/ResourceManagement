using App.BLL.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.DAL.Models;
using App.BLL.Services.Contracts;
using App.BLL.Services;
using Microsoft.AspNetCore.Authorization;

namespace App1.PL.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpPost("AddProject")]
        public Project AddProjects(Project p)
        {
            _projectService.AddProjects(p);
            return p;
        }
        [HttpGet("Projects")]
        public async Task<List<Project>> GetProjects()
        {
            List<Project> projects = await _projectService.GetProjects();
            return projects;
        }
        [HttpGet("SearchProject/{name}")]
        public async Task<Project> SearchProject(string name)
        {
            try
            {
                Project result = await _projectService.SearchProject(name);
                return result;
            }
            catch
            {
                throw;
            }
        }
        [HttpPatch("UpdateProject/{id}")]
        public string PatchProj(string Id, Project p)
        {
            _projectService.PatchProject(Id, p);
            return "succesfully updated";
        }
        [HttpDelete("DeleteProject/{id}")]
        public string DeleteProject(string Id)
        {
            this._projectService.DeleteProject(Id);
            return "deleted";
        }
    }
}
