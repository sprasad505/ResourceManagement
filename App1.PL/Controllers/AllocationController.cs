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
        public async Task<List<Allocation>> SearchAllocation(string id)
        {
<<<<<<< HEAD
            Allocation result = await allocationService.SearchAllocation(id);
            return result;
=======
            try
            {
              return await allocationService.SearchAllocation(id);
            }
            catch
            {
                throw;
            }
>>>>>>> 9c217ad5d6920d22715a57ae8f0e0dc73b33e031
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
