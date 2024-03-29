﻿using App.BLL.Services.Contracts;
using App.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App1.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService leaveService;
        public LeaveController(ILeaveService leaveService)
        {
            this.leaveService = leaveService;
        }
        [HttpGet("GetLeaves")]
        public async Task<List<Leave>> GetallLeaves()
        {
            return await leaveService.GetallLeaves();
        }
        [HttpGet("GetLeave/{EmployeeId}")]
        public async Task<List<Leave>> GetLeaves(string EmployeeId)
        {
            return await leaveService.GetLeaves(EmployeeId);
        }
        [HttpPost("AddLeave")]
        public ContentResult AddLeave(Leave l)
        {
            var data = this.leaveService.AddLeave(l);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpPatch("UpdateLeave/{id}")]
        public ContentResult PatchLeave(string Id, Leave l)
        {
            var data = this.leaveService.PatchLeave(Id, l);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpDelete("DeleteLeave/{id}")]
        public ContentResult DeleteLeave(string Id)
        {
            var data = this.leaveService.DeleteLeave(Id);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
    }
}
