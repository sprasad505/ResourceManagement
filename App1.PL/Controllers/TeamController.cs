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
        public string PatchTeam(string Id, Team t)
        {
            this.teamService.PatchTeam(Id, t);
            return "succesfully updated";
        }
        [HttpDelete("DeleteTeam/{id}")]
        public string DeleteTeam(string Id)
        {
            this.teamService.DeleteTeam(Id);
            return "deleted";
        }
    }
}
