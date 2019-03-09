using System;
using System.Collections.Generic;

namespace kyykt
{
    public partial class Student
    {
        public Student()
        {
            NeedToDo = new HashSet<NeedToDo>();
            Selection = new HashSet<Selection>();
            StuSignIn = new HashSet<StuSignIn>();
        }

        public string StudentId { get; set; }
        public string Sex { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Picture { get; set; }
        public string WxId { get; set; }

        public virtual ICollection<NeedToDo> NeedToDo { get; set; }
        public virtual ICollection<Selection> Selection { get; set; }
        public virtual ICollection<StuSignIn> StuSignIn { get; set; }
    }
}
