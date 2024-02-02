using ECX.Website.Application.DTOs.Email;
using ECX.Website.Application.Models;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        Task<ResponseAccount> SendEmail(SendEmailDto model);

    }
}
