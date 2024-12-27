using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.FeedBack
{
    public class FeedBackAnswerFormDto
    {
           public Guid Id { get; set; }
           public string? Answer { get; set; }

           public string? AnsweredBy { get; set; }
           public bool? requestSeen { get; set; }
          public bool? answerSeen { get; set; }
    }
}
