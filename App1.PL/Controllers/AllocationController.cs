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
    public class AllocationController : ControllerBase
    {
        private readonly IAllocationService allocationService;
        public AllocationController(IAllocationService allocationService)
        {
            this.allocationService = allocationService;
        }
        [HttpPost("AddAllocation")]
        public ContentResult AddAlloc(Allocation a)
        {
            var data = this.allocationService.AddAlloc(a);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpGet("Allocations")]
        public async Task<List<Allocation>> GetAllocations()
        {
            List<Allocation> allocations = await this.allocationService.GetAllocations();
            return allocations;
        }
        [HttpGet("SearchAllocation/{id}")]
        public async Task<List<Allocation>> SearchAllocation(string id)
        {
            return await allocationService.SearchAllocation(id);
        }
        [HttpPatch("UpdateAllocation/{id}")]
        public ContentResult PatchAlloc(string Id, Allocation a)
        {
            var data=allocationService.PatchAlloc(Id, a);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpDelete("DeleteAllocation/{id}")]
        public ContentResult DeleteAllocation(string Id)
        {
            var data=allocationService.DeleteAllocation(Id);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
    }
}
