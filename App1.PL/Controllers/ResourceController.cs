using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.DAL.Models;
using App.BLL.Services.Contracts;
using System.Diagnostics;

namespace App1.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpPost("Resources")]
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
        [HttpPatch("Resources")]
        public string PatchResource(string Id, Resource r)
        {
            this._resourceService.PatchResource(Id, r);
            return "done";
        }
        [HttpDelete("Resources")]
        public string DeleteResource(string Id)
        {
            this._resourceService.DeleteResource(Id);
            return "deleted";
        }
    }
}

