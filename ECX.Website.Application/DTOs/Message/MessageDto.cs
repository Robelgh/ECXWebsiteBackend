﻿using ECX.Website.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.Message
{
    public class MessageDto : BaseDtos
    {
        public Guid LangId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
