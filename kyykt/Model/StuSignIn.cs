using System;
using System.Collections.Generic;

namespace kyykt
{
    public partial class StuSignIn
    {
        public string ClassId { get; set; }
        public string StudentId { get; set; }
        public int Times { get; set; }
        public string SignIn { get; set; }

        public virtual OpeningClass Class { get; set; }
        public virtual Student Student { get; set; }
    }
}
