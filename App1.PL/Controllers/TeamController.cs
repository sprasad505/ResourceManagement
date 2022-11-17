using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.DAL.Models;
using App.BLL.Services.Contracts;

namespace App1.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService teamService;
        public TeamController(ITeamService teamService)
        {
            this.teamService = teamService;
        }
        [HttpPost("Teams")]
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
        [HttpPatch("Teams")]
        public string PatchTeam(string Id, Team t)
        {
            this.teamService.PatchTeam(Id, t);
            return "done";
        }
        [HttpDelete("Teams")]
        public string DeleteTeam(string Id)
        {
            this.teamService.DeleteTeam(Id);
            return "deleted";
        }
    }
}
