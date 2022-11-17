using App.BLL.Services.Contracts;
using App.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App1.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllocationController : ControllerBase
    {
        private readonly IAllocationService allocationService;
        public AllocationController(IAllocationService allocationService)
        {
            this.allocationService = allocationService;
        }
        [HttpPost("Allocations")]
        public Allocation AddAlloc(Allocation a)
        {
            this.allocationService.AddAlloc(a);
            return a;
        }
        [HttpGet("Allocations")]
        public async Task<List<Allocation>> GetAllocations()
        {
            List<Allocation> allocations = await this.allocationService.GetAllocations();
            return allocations;
        }
        [HttpPatch("Allocations")]
        public string PatchAlloc(string Id, Allocation a)
        {
            this.allocationService.PatchAlloc(Id, a);
            return "done";
        }
        [HttpDelete("Allocations")]
        public string DeleteAllocation(string Id)
        {
            this.allocationService.DeleteAllocation(Id);
            return "deleted";
        }
    }
}
