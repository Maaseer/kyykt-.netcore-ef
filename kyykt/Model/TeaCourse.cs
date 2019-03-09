using System;
using System.Collections.Generic;

namespace kyykt
{
    public partial class TeaCourse
    {
        public TeaCourse()
        {
            OpeningClass = new HashSet<OpeningClass>();
        }

        public string CourseId { get; set; }
        public string TeacherId { get; set; }
        public string Name { get; set; }
        public int? Credit { get; set; }
        public int? Hours { get; set; }
        public string Way { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<OpeningClass> OpeningClass { get; set; }
    }
}
