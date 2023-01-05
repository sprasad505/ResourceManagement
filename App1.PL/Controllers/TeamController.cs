using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.DAL.Models;
using App.BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace App1.PL.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService teamService;
        public TeamController(ITeamService teamService)
        {
            this.teamService = teamService;
        }
        [HttpPost("AddTeam"), Authorize(Roles = "0,4")]
        public ContentResult AddTeams(Team t)
        {
            var data = this.teamService.AddTeams(t);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpGet("Teams")]
        public async Task<List<Team>> GetTeams()
        {
            List<Team> teams = await this.teamService.GetTeams();
            return teams;
        }
        [HttpGet("SearchTeam/{id}")]
        public async Task<List<Team>> SearchTeam(string Id)
        {
            return await teamService.SearchTeam(Id);
        }
        [HttpPatch("UpdateTeam/{id}"), Authorize(Roles = "0,4")]
        public ContentResult PatchTeam(string Id, Team t)
        {
            var data=teamService.PatchTeam(Id, t);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpDelete("DeleteTeam/{id}"), Authorize(Roles = "0,4")]
        public ContentResult DeleteTeam(string Id)
        {
            var data=teamService.DeleteTeam(Id);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
    }
}
