using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.DAL.Models.Allocation;

namespace App.DAL.Models
{
    public class Alloc
    {
        public long Id { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public long? TeamId { get; set; }
        public string TeamName { get; set; }
        public long? ProjectId { get; set; }
        public Designation Role { get; set; }
        public double HoursPerDay { get; set; }
    }
}
