using System;
using System.Collections.Generic;

namespace kyykt
{
    public partial class ClassMessage
    {
        public int MessageId { get; set; }
        public string StudentId { get; set; }
        public string ClassId { get; set; }
        public string Message { get; set; }
        public DateTime MessageTime { get; set; }
        public string ReplyMessage { get; set; }
        public string MessageHead { get; set; }
        public int HasReply { get; set; }
    }
}
