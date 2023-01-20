using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Models
{
    public partial class Leave
    {
        public long Id { get; set; }
        public string EmployeeId { get; set; } = null!;
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime LeaveDate { get; set; }
        public int? hours { get; set; }

        public virtual Resource? Employee { get; set; } = null!;
    }
}

