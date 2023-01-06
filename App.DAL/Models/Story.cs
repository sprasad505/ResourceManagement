using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Models
{
    public partial class Story
    {
        public Story()
        {
            Points = new HashSet<Point>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? SprintId { get; set; }
        public long? ProjectId { get; set; }

        public virtual Project? Project { get; set; }
        public virtual Sprint? Sprint { get; set; }
        public virtual ICollection<Point> Points { get; set; }
    }
}
