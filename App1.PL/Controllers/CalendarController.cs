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
        public InterCalender AddHolidays(InterCalender c)
        {
            return this.calendarService.AddHolidays(c);
        }

        [HttpGet("GetHoliday")]
        public async Task<List<InterCalender>> GetHolidays()
        {
            List<Calendar22> calendar22s = await calendarService.GetHolidays();
            List<InterCalender> c = new List<InterCalender>();
            foreach (Calendar22 calendar22 in calendar22s)
            {
                string d = calendar22.Date.ToString("dd-MM-yyyy");
                c.Add(new InterCalender { Date = d, Name = calendar22.Name });
            }
            return c;
        }
    }
}
