using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kyykt.Model.QrCode
{
    public class QrClass
    {

    }
    public class QrToTeacher
    {
        public QrToTeacher(string qrCodePath, string qrCodeStr)
        {
            QrCodePath = qrCodePath;
            QrCodeStr = qrCodeStr;
        }

        public string QrCodePath { get; set; }
        public string QrCodeStr { get; set; }
    }
    public class QrToStudent
    {
        public QrToStudent(string StudentId, string QrCodeStr)
        {
            this.StudentId = StudentId;
            this.QrCodeStr = QrCodeStr;
        }

        public string StudentId { get; set; }
        public string QrCodeStr { get; set; }
    }

    public class TeacherToQr {
        public string TeacherId { get; set; }
        public string ClassId { get; set; }
    }

}
