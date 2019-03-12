using System;
using System.Collections.Generic;

namespace kyykt
{
    public partial class Selection
    {
        public string StudentId { get; set; }
        public string ClassId { get; set; }
        public int? ExaminationResults { get; set; }
        public int? UsualPerformance { get; set; }
        public int? TruancyTimes { get; set; }
        public int? Examination { get; set; }

        public virtual OpeningClass Class { get; set; }
        public virtual Student Student { get; set; }
    }
}
