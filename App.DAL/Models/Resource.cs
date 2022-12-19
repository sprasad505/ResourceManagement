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
        public string? Email { get; set; }
        public string Name { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public long? ProjectId { get; set; }

        public virtual Project? Project { get; set; }
        public virtual ICollection<Allocation> Allocations { get; set; }
        public virtual ICollection<Leave> Leaves { get; set; }

    }
}
