﻿using ECX.Website.Application.Contracts.Infrastructure;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.DTOs.Account;
using ECX.Website.Application.DTOs.Email;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;

namespace ECX.Website.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
      
        private UserManager<IdentityUser> _userManger;
        private IConfiguration _configuration;
        private IEmailSender _mailService;

        static Account user;
        BaseCommonResponse response;
        AuthenticationCommandResponse response2;




        public AccountRepository(UserManager<IdentityUser> userManager, IConfiguration configuration, IEmailSender mailService)
        {
            _userManger = userManager;
            _configuration = configuration;
            _mailService= mailService;

        }

        public async Task<ResponseAccount> RegisterUserAsync(Account model , string password)
        {


            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.FirstName,
            };

          

            model.UserName = model.FirstName;
            var result = await _userManger.CreateAsync(model, password);

            if (result.Succeeded)
            {

                //var confirmEmailToken = await _userManger.GenerateEmailConfirmationTokenAsync(identityUser);

                //var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                //var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                //string url = $"{_configuration["AppUrl"]}/api/auth/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

                //var mailModel = new SendEmailDto
                //{
                //    To = model.Email,
                //    Subject = "Confirm your email",
                //    Body = $"<h1>Welcome to Auth Demo</h1>" + $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>",
                //};

                //await _mailService.SendEmail(mailModel);

               

                //await _mailService.SendEmailAsync(identityUser.Email, "Confirm your email", $"<h1>Welcome to Auth Demo</h1>" +
                //    $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");


                return new ResponseAccount
                {
                    Message = "User created successfully!",
                    Success = true,
                };
            }

            return new ResponseAccount
            {
                Message = "User did not create",
                Success = false,
                Errors = result.Errors.Select(e => e.Description)
            };


        }

        public async Task<ResponseAccount> LoginUserAsync(loginDto model)
        {

            var user = await _userManger.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new ResponseAccount
                {
                    Message = "There is no user with that Email address",
                    Success = false,
                };
            }

            var result = await _userManger.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new ResponseAccount
                {
                    Message = "Invalid password",
                    Success = false,
                };

            var claims = new[]
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:securityKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:validIssuer"],
                audience: _configuration["AuthSettings:validAudience"],
                claims: claims, 
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new ResponseAccount
            {
                Message = tokenAsString,
                UserName = model.Email,
                Success = true,
                ExpireDate = token.ValidTo
            };
        }


        public async Task<ResponseAccount> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManger.FindByIdAsync(userId);
            if (user == null)
                return new ResponseAccount
                {
                    Success = false,
                    Message = "User not found"
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManger.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new ResponseAccount
                {
                    Message = "Email confirmed successfully!",
                    Success = true,
                };

            return new ResponseAccount
            {
                Success = false,
                Message = "Email did not confirm",
                Errors = result.Errors.Select(e => e.Description)
            };

        }

        public async Task<ResponseAccount> ForgetPasswordAsync(string email)
        {

            var user = await _userManger.FindByEmailAsync(email);
            if (user == null)
                return new ResponseAccount
                {
                    Success = false,
                    Message = "No user associated with email",
                };

            var token = await _userManger.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["AppUrl"]}/ResetPassword?email={email}&token={validToken}";

            //await _mailService.SendEmailAsync(email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
            //    $"<p>To reset your password <a href='{url}'>Click here</a></p>");

            return new ResponseAccount
            {
                Success = true,
                Message = "Reset password URL has been sent to the email successfully!"
            };
        }

        public async Task<ResponseAccount> ResetPasswordAsync(ResetPasswordDto model)
        {

            var user = await _userManger.FindByEmailAsync(model.Email);
            if (user == null)
                return new ResponseAccount
                {
                    Success = false,
                    Message = "No user associated with email",
                };

            if (model.NewPassword != model.ConfirmPassword)
                return new ResponseAccount
                {
                    Success = false,
                    Message = "Password doesn't match its confirmation",
                };

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManger.ResetPasswordAsync(user, normalToken, model.NewPassword);

            if (result.Succeeded)
                return new ResponseAccount
                {
                    Message = "Password has been reset successfully!",
                    Success = true,
                };

            return new ResponseAccount
            {
                Message = "Something went wrong",
                Success = false,
                Errors = result.Errors.Select(e => e.Description),
            };
        }

        public async Task<AuthenticationCommandResponse> AuthenticateUser(string userName, string password)
        {
            response2 = new AuthenticationCommandResponse();
            if (AutenticateToAD(_configuration["DirPath"], _configuration["domain"], userName, password, _configuration["ACDUser"], _configuration["ACDPass"]))
            {
                response2.Success = true;
                response2.Token = CreateToken();
                response2.UserId = user.Id;
                response2.UserName = user.UserName;
       

                return response2;
            }
            else
            {
                response2.Success = false;
                response2.Message = "Incorrect User Name or Password";
                return response2;
            }

        }

        bool AutenticateToAD(string dirPath, string _domain, string userName, string pwd, string _adAdminUser, string _adAdminPass)
        {
            // GetByOrganizationalUnit("IT");
            string domain = _domain;
            string LDAP_Path = dirPath;
            string adAdminUser = _adAdminUser;
            string adAdminPass = _adAdminPass;

            if (string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(LDAP_Path))
                return false;
            string domainAndUsername = domain + @"\" + userName;

            try
            {
                #region Authenticate using Directory Search

                using (System.DirectoryServices.DirectoryEntry entry = new(LDAP_Path, userName, pwd, AuthenticationTypes.Secure | AuthenticationTypes.Sealing | AuthenticationTypes.Signing))
                {

                    object obj = entry.NativeObject;
                    DirectorySearcher search = new DirectorySearcher(entry);

                    search.Filter = "(sAMAccountName=" + userName + ")";
                    search.PropertiesToLoad.Add("CN");
                    search.PropertiesToLoad.Add("distinguishedName");
                    SearchResultCollection results = search.FindAll();
                    if (results == null || results.Count == 0)
                    {
                        return false;
                    }
                    if (results.Count > 1)
                    {
                        results.Dispose();
                        return false;
                    }
                    SearchResult result = results[0];

                    if (result != null)
                    {
                        string distinguishedName = result.Properties["distinguishedName"][0].ToString();

                        System.DirectoryServices.DirectoryEntry userADEntry = result.GetDirectoryEntry();
                        user = new();
                        user.Id = userADEntry.Guid;
                        user.UserName = userADEntry.Username;
                       
                    }
                    else
                    {
                        return false;
                    }
                    entry.Close();
                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        string CreateToken()
        {

            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            try
            {
                var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            expires: DateTime.UtcNow.AddMinutes(1),
            signingCredentials: signIn);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        static string GetOrganizationalUnit(string distinguishedName)
        {
            string[] parts = distinguishedName.Split(',');

            foreach (string part in parts)
            {
                if (part.StartsWith("OU="))
                {
                    return part.Substring(3);
                }
            }

            return "No OU found";

        }

  


    }
}
