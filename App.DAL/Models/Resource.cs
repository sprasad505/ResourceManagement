using System;
using System.Collections.Generic;

namespace App.DAL.Models
{
    public partial class Resource
    {
        public Resource()
        {
            Allocations = new HashSet<Allocation>();
        }

        public long Id { get; set; }
        public string EmployeeId { get; set; } = null!;
        public string  Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Designation { get; set; } = null!;

        public virtual ICollection<Allocation> Allocations { get; set; }
    }
}
