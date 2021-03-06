﻿using System;
using System.Collections.Generic;

namespace kyykt
{
    public partial class Notice
    {
        public int NoticeId { get; set; }
        public string Content { get; set; }
        public string ClassId { get; set; }
        public string Head { get; set; }
        public DateTime? Time { get; set; }

        public virtual OpeningClass Class { get; set; }

    }
}
