using System;
using System.Collections.Generic;

namespace kyykt
{
    public partial class NeedToDo
    {
        public int NeedToDoId { get; set; }
        public string StudentId { get; set; }
        public string Finish { get; set; }
        public string Content { get; set; }
        public string Hide { get; set; }
        public string Time { get; set; }
        public virtual Student Student { get; set; }
        public string Extra { get; set; }
    }
}
