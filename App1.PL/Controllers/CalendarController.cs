using App.BLL.Services;
using App.BLL.Services.Contracts;
using App.DAL.DataContext;
using App.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace App1.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService calendarService;
        public CalendarController(ICalendarService calendarService)
        {
            this.calendarService = calendarService;
        }
        [HttpPost("AddHoliday")]
        public Calendar22 AddHolidays(Calendar22 c)
        {
            return this.calendarService.AddHolidays(c);
        }

        [HttpGet("GetHoliday")]
        public async Task<List<Calendar22>> GetHolidays()
        {
            List<Calendar22> calendar22s = await calendarService.GetHolidays();
            return calendar22s;
        }
    }
}
