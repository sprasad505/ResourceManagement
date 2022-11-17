using App.BLL.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.DAL.Models;
using App.BLL.Services.Contracts;

namespace App1.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpPost("Projects")]
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
        [HttpPatch("Projects")]
        public string PatchProj(string Id, Project p)
        {
            _projectService.PatchProject(Id, p);
            return "done";
        }
        [HttpDelete("Projects")]
        public string DeleteProject(string Id)
        {
            this._projectService.DeleteProject(Id);
            return "deleted";
        }
    }
}
