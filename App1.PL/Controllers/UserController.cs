using App.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App1.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AllocationService _locationService;
        public UserController(AllocationService locationService)
        {
            _locationService = locationService;
        }

       /* [HttpPost]
        public string Addadmin(string username, string password)
        {
            _locationService.
        }*/
    }
}
