﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.Contracts.Persistence
{
    public interface IRealTimeDataRepository
    {
        Task SendRealTimeData(string data);
    }
}
