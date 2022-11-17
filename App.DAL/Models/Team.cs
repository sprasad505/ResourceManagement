using System;
using System.Collections.Generic;

namespace App.DAL.Models
{
    public partial class Team
    {
        public Team()
        {
            Allocations = new HashSet<Allocation>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Allocation> Allocations { get; set; }
    }
}
