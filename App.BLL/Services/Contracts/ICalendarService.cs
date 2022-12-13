using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services.Contracts
{
    public interface ICalendarService
    {
        public string AddHolidays(InterCalender c);
        Task<List<Calendar22>> GetHolidays();
    }
}
