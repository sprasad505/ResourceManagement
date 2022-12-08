using System;
using System.Collections.Generic;

namespace App.DAL.Models
{
    public partial class Project
    {
        public Project()
        {
            Allocations = new HashSet<Allocation>();
            Resources = new HashSet<Resource>();
            Sprints = new HashSet<Sprint>();
            Stories = new HashSet<Story>();
            Teams = new HashSet<Team>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Allocation> Allocations { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
        public virtual ICollection<Sprint> Sprints { get; set; }
        public virtual ICollection<Story> Stories { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
