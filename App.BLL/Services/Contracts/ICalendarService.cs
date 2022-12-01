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
        public Calendar22 AddHolidays(Calendar22 c);
        Task<List<Calendar22>> GetHolidays();
    }
}
