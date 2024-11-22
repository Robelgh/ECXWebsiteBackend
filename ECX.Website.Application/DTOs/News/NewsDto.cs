﻿using ECX.Website.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.News
{
    public class NewsDto : BaseDtos
    {
        public Guid LangId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public DateTime ExpDate { get; set; }
        public string ImgName { get; set; }
    }
}
