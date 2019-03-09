using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kyykt.Model
{
    public class ClassTable
    {
        public string CourseName { get; set; }
        public List<ClassTime> ClassTime { get; set; }
        public string CourseId { get; set; }
    }
}
