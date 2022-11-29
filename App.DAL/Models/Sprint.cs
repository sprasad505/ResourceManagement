using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Models
{
    public  partial class Sprint
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public long Duration { get; set; }
    }
}
