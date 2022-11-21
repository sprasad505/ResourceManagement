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
        [HttpPost("AddTeam")]
        public Team AddTeams(Team t)
        {
            this.teamService.AddTeams(t);
            return t;
        }
        [HttpGet("Teams")]
        public async Task<List<Team>> GetTeams()
        {
            List<Team> teams = await this.teamService.GetTeams();
            return teams;
        }
        [HttpGet("SearchTeam/{name}")]
        public async Task<Team> SearchTeam(string name)
        {
            try
            {
                Team result = await teamService.SearchTeam(name);
                return result;
            }
            catch
            {
                throw;
            }
        }
        [HttpPatch("UpdateTeam/{id}")]
        public ContentResult PatchTeam(string Id, Team t)
        {
            var data=teamService.PatchTeam(Id, t);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpDelete("DeleteTeam/{id}")]
        public ContentResult DeleteTeam(string Id)
        {
            var data=0teamService.DeleteTeam(Id);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
    }
}
