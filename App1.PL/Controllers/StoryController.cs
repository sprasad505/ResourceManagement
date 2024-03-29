﻿using App.BLL.Services.Contracts;
using App.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App1.PL.Controllers
{
    [Authorize]
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
        [HttpPost("AddStory"), Authorize(Roles = "0,4")]
        public ContentResult AddStory(Story s)
        {
            var data = this.storyService.AddStory(s);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpGet("SearchStory/{id}")]
        public async Task<List<Story>> SearchStory(string Id)
        {
            return await storyService.SearchStory(Id);
        }
        [HttpGet("GetAddedStories/{id}")]
        public async Task<List<Story>> GetAddedStories(string Id)
        {
            return await storyService.GetAddedStories(Id);
        }
        [HttpGet("StoriesLeft/{id}")]
        public async Task<List<Story>> StoriesLeft(string Id)
        {
            return await storyService.StoriesLeft(Id);
        }
        [HttpPatch("UpdateStory/{id}"), Authorize(Roles = "0,4")]
        public ContentResult PatchStory(string Id, Story s)
        {
            var data = this.storyService.PatchStory(Id, s);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpPatch("AddStorytoSprint"), Authorize(Roles = "0,4")]
        public List<Story> AddStorytoSprint(List<Story> s)
        {
            return this.storyService.AddStorytoSprint(s);
        }
        [HttpDelete("DeleteStory/{id}"), Authorize(Roles = "0,4")]
        public ContentResult DeleteStory(string Id)
        {
            var data = this.storyService.DeleteStory(Id);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }

    }
}
