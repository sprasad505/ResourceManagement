using App.BLL.Services.Contracts;
using App.DAL.Models;
using App.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IGenericRepository<Calendar22> genericrepository;
        public CalendarService(IGenericRepository<Calendar22> genericrepository)
        {
            this.genericrepository = genericrepository;
        }

        public InterCalender AddHolidays(InterCalender c)
        {
            try
            {
                return this.genericrepository.AddHolidays(c);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Calendar22>> GetHolidays()
        {
            try
            {
                return await this.genericrepository.GetHolidays();
            }
            catch
            {
                throw;
            }
        }
    }
}
