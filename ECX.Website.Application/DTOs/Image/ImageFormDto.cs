﻿using ECX.Website.Application.DTOs.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.Image
{
    public class ImageFormDto : BaseDtos
    {
        public Guid LangId { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public bool   IsCarousel { get; set; }
        public IFormFile ImgFile{get;set;}
    }
}
