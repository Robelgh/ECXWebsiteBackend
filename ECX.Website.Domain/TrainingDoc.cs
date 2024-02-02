﻿using ECX.Website.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECX.Website.Domain
{
    public class TrainingDoc : BaseDomainEntity
    {

        public Guid LangId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }

    }
}
