using System;
using System.Collections.Generic;

namespace kyykt
{
    public partial class StudentSignIn
    {
        public StudentSignIn(string signInCode, string studentId)
        {
            SignInCode = signInCode;
            StudentId = studentId;
        }

        public string SignInCode { get; set; }
        public string StudentId { get; set; }
    }
}
