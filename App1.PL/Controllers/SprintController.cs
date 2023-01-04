using App.BLL.Services.Contracts;
using App.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App1.PL.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SprintController : ControllerBase
    {
        private readonly ISprintService sprintService;
        public SprintController(ISprintService sprintService)
        {
            this.sprintService = sprintService;
        }
        [HttpGet("Sprints")]
        public async Task<List<Sprint>> GetSprints()
        {
            List<Sprint> sprints = await sprintService.GetSprints();
            return sprints;
        }
        [HttpPost("AddSprint"), Authorize(Roles = "Admin,ScrumMaster")]
        public ContentResult AddSprint(Sprint s)
        {
            var data = this.sprintService.AddSprints(s);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpGet("SearchSprint/{id}")]
        public async Task<List<Sprint>> SearchSprint(string id)
        {
            return await this.sprintService.SearchSprint(id);  
        }
        [HttpPatch("UpdateSprint/{id}"), Authorize(Roles = "Admin,ScrumMaster")]
        public ContentResult PatchSprint(long Id, Sprint s)
        {
            var data = this.sprintService.PatchSprint(Id, s);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpDelete("DeleteSprint/{id}"), Authorize(Roles = "Admin,ScrumMaster")]
        public ContentResult DeleteSprint(string Id)
        {
            var data = this.sprintService.DeleteSprint(Id);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
    }
}
