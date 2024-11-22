using ECX.Website.Application.DTOs.Account;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.Contracts.Persistence
{
    public interface IAccountRepository 
    {
        Task<ResponseAccount> RegisterUserAsync(Account model , string password);

        Task<ResponseAccount> LoginUserAsync(loginDto model);

        Task<ResponseAccount> ConfirmEmailAsync(string userId, string token);

        Task<ResponseAccount> ForgetPasswordAsync(string email);

        Task<ResponseAccount> ResetPasswordAsync(ResetPasswordDto model);

    }
}