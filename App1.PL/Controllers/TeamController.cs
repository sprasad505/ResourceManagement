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
        [HttpGet("SearchTeam/{id}")]
        public async Task<List<Team>> SearchTeam(string Id)
        {
<<<<<<< HEAD
            Team result = await teamService.SearchTeam(name);
            return result;
=======
            try
            {
                return await teamService.SearchTeam(Id);
            }
            catch
            {
                throw;
            }
>>>>>>> 9c217ad5d6920d22715a57ae8f0e0dc73b33e031
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
            var data=teamService.DeleteTeam(Id);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
    }
}
