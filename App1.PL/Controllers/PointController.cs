using App.BLL.Services.Contracts;
using App.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App1.PL.Controllers
{
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
        [HttpPost("AddPoint")]
        public Point AddPoint(Point po)
        {
            return this.pointService.AddPoint(po);
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
