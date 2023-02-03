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
    public class PointController : ControllerBase
    {
        private readonly IPointService pointService;
        public PointController(IPointService pointService)
        {
            this.pointService = pointService;
        }
        [HttpGet("Points")]
        public async Task<List<Point>> GetPoint()
        {
            List<Point> points = await pointService.GetPoints();
            return points;
        }
        [HttpGet("SearchPoint/{id}")]
        public async Task<List<Point>> SearchPoint(string Id)
        {
            List<Point> points = await pointService.SearchPoint(Id);
            return points;
        }
        [HttpPost("AddPoint")]
        public ContentResult AddPoint(List<Point> po)
        {
            var data = this.pointService.AddPoint(po);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpPatch("UpdatePoint/{id}")]
        public ContentResult PatchPoint(string Id, Point po)
        {
            var data = this.pointService.PatchPoint(Id, po);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
        [HttpDelete("DeletePoint/{id}")]
        public ContentResult DeletePoint(string Id)
        {
            var data = this.pointService.DeletePoint(Id);
            return Content(data.ToString(), "application/json", System.Text.Encoding.UTF8);
        }
    }
}
