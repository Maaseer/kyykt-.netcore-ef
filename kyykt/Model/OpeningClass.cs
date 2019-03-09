using System;
using System.Collections.Generic;

namespace kyykt
{
    public partial class OpeningClass
    {
        public OpeningClass()
        {
            ClassTime = new HashSet<ClassTime>();
            Notice = new HashSet<Notice>();
            Selection = new HashSet<Selection>();
            StuSignIn = new HashSet<StuSignIn>();
        }

        public string ClassId { get; set; }
        public string CourseId { get; set; }
        public int? Times { get; set; }
        public string CloseClass { get; set; }

        public virtual TeaCourse Course { get; set; }
        public virtual ICollection<ClassTime> ClassTime { get; set; }
        public virtual ICollection<Notice> Notice { get; set; }
        public virtual ICollection<Selection> Selection { get; set; }
        public virtual ICollection<StuSignIn> StuSignIn { get; set; }
    }
}
