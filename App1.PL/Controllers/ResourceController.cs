﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.DAL.Models;
using App.BLL.Services.Contracts;
using System.Diagnostics;
using App.BLL.Services;
using Microsoft.AspNetCore.Authorization;

namespace App1.PL.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }
        [HttpPost("AddScrumMaster"), Authorize(Roles = "4")]
        public ContentResult AddScrumMaster(Resource r)
        {
            var data = this._resourceService.AddScrumMaster(r);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpGet("GetScrumMasters"), Authorize(Roles= "4")]
        public async Task<List<Resource>> GetScrumMaster()
        {
            List<Resource> scrum = await this._resourceService.GetScrumMaster();
            return scrum;
        }

        [HttpPost("AddResource")]
        public ContentResult Addresources(Resource r)
        {
            var data = this._resourceService.AddResources(r);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpGet("Resources")]
        public async Task<List<Resource>> GetResources()
        {
            List<Resource> resources = await _resourceService.GetResources();
            return resources;
        }
        [HttpGet("SearchResource/{id}")]
        public async Task<List<Resource>> SearchResource(string id)
        {
            return await _resourceService.SearchResource(id);
        }
        [HttpPatch("UpdateResource/{id}")]
        public ContentResult PatchResource(string Id, Resource r)
        {
            var data=_resourceService.PatchResource(Id, r);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpDelete("DeleteResource/{id}")]
        public ContentResult DeleteResource(string Id)
        {
            var data=_resourceService.DeleteResource(Id);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
    }
}

