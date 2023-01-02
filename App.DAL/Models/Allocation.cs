﻿using System;
using System.Collections.Generic;

namespace App.DAL.Models
{
    public partial class Allocation
    {
        public long Id { get; set; }
        public string? EmployeeId { get; set; }
        public long? TeamId { get; set; }
        public long? ProjectId { get; set; }
        public  Designation Role { get; set; }
        public double HoursPerDay { get; set; }
        public virtual Resource? Employee { get; set; }
        public virtual Project? Project { get; set; }
        public virtual Team? Team { get; set; }

        public enum Designation
        {
            ScrumMaster,
            Lead,
            Developer,
            QAEngineer,
            Admin
        }
    }
}
