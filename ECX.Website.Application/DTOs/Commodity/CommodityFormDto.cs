﻿using ECX.Website.Application.DTOs.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.Commodity
{
    public class CommodityFormDto : BaseDtos
    {
        public Guid LangId { get; set; }
        public Guid CommodityGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? ImgFile{get;set;}
    }
}
