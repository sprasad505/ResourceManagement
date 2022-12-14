using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Models
{
    public  partial class Sprint
    {
        public Sprint()
        {
            Stories = new HashSet<Story>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long Duration { get; set; }
        public long? ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        public virtual ICollection<Story> Stories { get; set; }
    }
}
