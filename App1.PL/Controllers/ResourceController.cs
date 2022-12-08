using Microsoft.AspNetCore.Http;
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

        [HttpPost("AddResource")]
        public Resource Addresources(Resource r)
        {
            _resourceService.AddResources(r);
            return r;
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
            try
            {
               return await _resourceService.SearchResource(id);
            }
            catch
            {
                throw;
            }
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

