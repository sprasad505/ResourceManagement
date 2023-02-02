using App.BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public ContentResult AddProjects(Project p, [FromHeader]string id)
        {
            var data = this._projectService.AddProjects(p,id);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpGet("Projects"), Authorize(Roles = "4")]
        public async Task<List<Project>> GetProjects()
        {
            List<Project> projects = await _projectService.GetProjects();
            return projects;
        }
        [HttpGet("UserProjects/{id}")]
        public async Task<List<Project>> SearchProject(string id)
        {
            List<Project> projects = await _projectService.SearchProject(id);
            return projects;
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
