using App.BLL.Services.Contracts;
using App.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App1.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService storyService;
        public StoryController(IStoryService storyService)
        {
            this.storyService = storyService;
        }
        [HttpGet("Stories")]
        public async Task<List<Story>> GetStory()
        {
            List<Story> stories = await storyService.GetStories();
            return stories;
        }
        [HttpPost("AddStory")]
        public Story AddStory(Story s)
        {
            return this.storyService.AddStory(s);
        }
        [HttpPatch("UpdateStory/{id}")]
        public ContentResult PatchStory(string Id, Story s)
        {
            var data = this.storyService.PatchStory(Id, s);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpDelete("DeleteStory/{id}")]
        public ContentResult DeleteStory(string Id)
        {
            var data = this.storyService.DeleteStory(Id);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
    }
}
