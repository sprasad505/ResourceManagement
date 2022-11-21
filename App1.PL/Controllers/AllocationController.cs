using App.BLL.Services.Contracts;
using App.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App1.PL.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AllocationController : ControllerBase
    {
        private readonly IAllocationService allocationService;
        public AllocationController(IAllocationService allocationService)
        {
            this.allocationService = allocationService;
        }
        [HttpPost("AddAllocation")]
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
        [HttpGet("SearchAllocation/{id}")]
        public async Task<Allocation> SearchAllocation(string id)
        {
            try
            {
                Allocation result = await allocationService.SearchAllocation(id);
                return result;
            }
            catch
            {
                throw;
            }
        }
        [HttpPatch("UpdateAllocation/{id}")]
        public string PatchAlloc(string Id, Allocation a)
        {
            this.allocationService.PatchAlloc(Id, a);
            return "succesfully updated";
        }
        [HttpDelete("DeleteAllocation/{id}")]
        public string DeleteAllocation(string Id)
        {
            this.allocationService.DeleteAllocation(Id);
            return "deleted";
        }
    }
}
