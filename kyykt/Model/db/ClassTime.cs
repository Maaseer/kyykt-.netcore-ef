using System;
using System.Collections.Generic;

namespace kyykt
{
    public partial class ClassTime
    {
        public ClassTime(string time, string place)
        {
            Time = time;
            Place = place;
        }

        public ClassTime(string time, string place, string classId)
        {
            Time = time;
            Place = place;
            ClassId = classId;
        }

        public string Time { get; set; }
        public string Place { get; set; }
        public string ClassId { get; set; }

        public virtual OpeningClass Class { get; set; }
    }
}
