using System;
using System.Collections.Generic;

namespace kyykt
{
    public partial class ClassSignIn
    {
        public ClassSignIn(string signInCode, string classId, DateTime? signInDate, int signNum)
        {
            SignInCode = signInCode;
            ClassId = classId;
            SignInDate = signInDate;
            SignNum = signNum;
        }

        public string SignInCode { get; set; }
        public string ClassId { get; set; }
        public DateTime? SignInDate { get; set; }
        public int? IsOverDue { get; set; }
        public int SignNum { get; set; }
    }
}
