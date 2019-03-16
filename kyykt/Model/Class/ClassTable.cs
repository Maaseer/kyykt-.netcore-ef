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

    public class StudentInClass
    {
        public StudentInClass(string studentId, string studentName, string studentTel, DateTime studentSignInTime, bool studentHasSignIn = false)
        {
            StudentId = studentId;
            StudentName = studentName;
            StudentTel = studentTel;
            StudentHasSignIn = studentHasSignIn;
            StudentSignInTime = studentSignInTime;
        }

        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentTel { get; set; }
        public bool StudentHasSignIn { get; set; }
        public DateTime StudentSignInTime { get; set; }
    }

    public class TeacherGetStudentSign
    {
        public string ClassId { get; set; }
        public int SignInNums { get; set; }
    }
}
