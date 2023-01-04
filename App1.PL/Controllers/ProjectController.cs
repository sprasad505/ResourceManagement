using App.BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.DAL.Models;
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
        [HttpPost("AddProject"), Authorize(Roles = "4,0")]
        public ContentResult AddProjects(Project p)
        {
            var data = this._projectService.AddProjects(p);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
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
            Project result = await _projectService.SearchProject(name);
            return result;
        }
        [HttpPatch("UpdateProject/{id}"), Authorize(Roles = "4,0")]
        public ContentResult PatchProj(string Id, Project p)
        {
             var data=_projectService.PatchProject(Id, p);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpDelete("DeleteProject/{id}"), Authorize(Roles = "4,0")]
        public ContentResult DeleteProject(string Id)
        {
            var data=_projectService.DeleteProject(Id);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
    }
}
