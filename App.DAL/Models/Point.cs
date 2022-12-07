using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Models
{
    public partial class Point
    {
        public long Id { get; set; }
        public long StoryId { get; set; }
        public int Points { get; set; }
        public long UserId { get; set; }
        public virtual Story? Story { get; set; } = null!;
    }
}
