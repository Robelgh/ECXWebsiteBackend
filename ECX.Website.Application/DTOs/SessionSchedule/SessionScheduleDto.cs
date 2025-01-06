using ECX.Website.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.SessionSchedule
{
    public class SessionScheduleDto : BaseDtos
    {
        public Guid LangId { get; set; }
        public int Session { get; set; }
        public string Name { get; set; }
        public DateTime Open { get; set; }
        public DateTime Close { get; set; }
    }
}
