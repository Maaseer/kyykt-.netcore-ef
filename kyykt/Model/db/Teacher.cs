using System;
using System.Collections.Generic;

namespace kyykt
{
    public partial class Teacher
    {
        public Teacher()
        {
            TeaCourse = new HashSet<TeaCourse>();
        }

        public string TeacherId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Occupation { get; set; }
        public string Picture { get; set; }
        public string WxId { get; set; }

        public string TeacherPasswd { get; set;  }
        public virtual ICollection<TeaCourse> TeaCourse { get; set; }
    }
}
