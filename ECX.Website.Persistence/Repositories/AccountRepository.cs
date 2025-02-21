using Azure;
using ECX.Website.Application.Contracts.Infrastructure;
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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;

using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices;
using System.Security.Cryptography;
using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;
using System.Dynamic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace ECX.Website.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
      
       // private UserManager<IdentityUser> _userManger;
        private IConfiguration _configuration;
        dynamic userModel = new System.Dynamic.ExpandoObject();
        private IEmailSender _mailService;
        ResponseAccount response;
        LoginTrader loginTrader;

        private readonly IHttpContextAccessor _httpContext;

        public AccountRepository(
          //  UserManager<IdentityUser> userManager, 
            IConfiguration configuration, 
            IEmailSender mailService, 
            IHttpContextAccessor httpContextAccessor)
        {
         //   _userManger = userManager;
            _configuration = configuration;
            _mailService= mailService;
            _httpContext = httpContextAccessor;

        }

        //public async Task<ResponseAccount> RegisterUserAsync(Account model , string password)
        //{


        //    var identityUser = new IdentityUser
        //    {
        //        Email = model.Email,
        //        UserName = model.FirstName,
        //    };

        //    model.UserName = model.FirstName;
        //    var result = await _userManger.CreateAsync(model, password);

        //    if (result.Succeeded)
        //    {

        //        //var confirmEmailToken = await _userManger.GenerateEmailConfirmationTokenAsync(identityUser);

        //        //var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
        //        //var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

        //        //string url = $"{_configuration["AppUrl"]}/api/auth/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

        //        //var mailModel = new SendEmailDto
        //        //{
        //        //    To = model.Email,
        //        //    Subject = "Confirm your email",
        //        //    Body = $"<h1>Welcome to Auth Demo</h1>" + $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>",
        //        //};

        //        //await _mailService.SendEmail(mailModel);

               

        //        //await _mailService.SendEmailAsync(identityUser.Email, "Confirm your email", $"<h1>Welcome to Auth Demo</h1>" +
        //        //    $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");


        //        return new ResponseAccount
        //        {
        //            Message = "User created successfully!",
        //            Success = true,
        //        };
        //    }

        //    return new ResponseAccount
        //    {
        //        Message = "User did not create",
        //        Success = false,
        //        Errors = result.Errors.Select(e => e.Description)
        //    };


        //}

        //public async Task<ResponseAccount> LoginUserAsync(LoginADDto model)
        //{

        //    var user = await _userManger.FindByEmailAsync(model.UserName);

        //    if (user == null)
        //    {
        //        return new ResponseAccount
        //        {
        //            Message = "There is no user with that Email address",
        //            Success = false,
        //        };
        //    }

        //    var result = await _userManger.CheckPasswordAsync(user, model.Password);

        //    if (!result)
        //        return new ResponseAccount
        //        {
        //            Message = "Invalid password",
        //            Success = false,
        //        };

        //    var claims = new[]
        //    {
        //        new Claim("UserName", model.UserName),
        //        new Claim(ClaimTypes.NameIdentifier, user.Id),
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:securityKey"]));

        //    var token = new JwtSecurityToken(
        //        issuer: _configuration["AuthSettings:validIssuer"],
        //        audience: _configuration["AuthSettings:validAudience"],
        //        claims: claims, 
        //        expires: DateTime.Now.AddMinutes(30),
        //        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        //    string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

        //    return new ResponseAccount
        //    {
        //        Message = tokenAsString,
        //        UserName = model.UserName,
        //        Success = true,
        //        ExpireDate = token.ValidTo
        //    };
        //}


        //public async Task<ResponseAccount> ConfirmEmailAsync(string userId, string token)
        //{
        //    var user = await _userManger.FindByIdAsync(userId);
        //    if (user == null)
        //        return new ResponseAccount
        //        {
        //            Success = false,
        //            Message = "User not found"
        //        };

        //    var decodedToken = WebEncoders.Base64UrlDecode(token);
        //    string normalToken = Encoding.UTF8.GetString(decodedToken);

        //    var result = await _userManger.ConfirmEmailAsync(user, normalToken);

        //    if (result.Succeeded)
        //        return new ResponseAccount
        //        {
        //            Message = "Email confirmed successfully!",
        //            Success = true,
        //        };

        //    return new ResponseAccount
        //    {
        //        Success = false,
        //        Message = "Email did not confirm",
        //        Errors = result.Errors.Select(e => e.Description)
        //    };

        //}

        //public async Task<ResponseAccount> ForgetPasswordAsync(string email)
        //{

        //    var user = await _userManger.FindByEmailAsync(email);
        //    if (user == null)
        //        return new ResponseAccount
        //        {
        //            Success = false,
        //            Message = "No user associated with email",
        //        };

        //    var token = await _userManger.GeneratePasswordResetTokenAsync(user);
        //    var encodedToken = Encoding.UTF8.GetBytes(token);
        //    var validToken = WebEncoders.Base64UrlEncode(encodedToken);

        //    string url = $"{_configuration["AppUrl"]}/ResetPassword?email={email}&token={validToken}";

        //    //await _mailService.SendEmailAsync(email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
        //    //    $"<p>To reset your password <a href='{url}'>Click here</a></p>");

        //    return new ResponseAccount
        //    {
        //        Success = true,
        //        Message = "Reset password URL has been sent to the email successfully!"
        //    };
        //}

        //public async Task<ResponseAccount> ResetPasswordAsync(ResetPasswordDto model)
        //{

        //    var user = await _userManger.FindByEmailAsync(model.Email);
        //    if (user == null)
        //        return new ResponseAccount
        //        {
        //            Success = false,
        //            Message = "No user associated with email",
        //        };

        //    if (model.NewPassword != model.ConfirmPassword)
        //        return new ResponseAccount
        //        {
        //            Success = false,
        //            Message = "Password doesn't match its confirmation",
        //        };

        //    var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
        //    string normalToken = Encoding.UTF8.GetString(decodedToken);

        //    var result = await _userManger.ResetPasswordAsync(user, normalToken, model.NewPassword);

        //    if (result.Succeeded)
        //        return new ResponseAccount
        //        {
        //            Message = "Password has been reset successfully!",
        //            Success = true,
        //        };

        //    return new ResponseAccount
        //    {
        //        Message = "Something went wrong",
        //        Success = false,
        //        Errors = result.Errors.Select(e => e.Description),
        //    };
        //}

        public async Task<ResponseAccount> AutenticateUser(string userName, string password)
        {
            response = new ResponseAccount();
            if (AutenticateToAD(_configuration["DirPath"], _configuration["domain"], userName, password, _configuration["ACDUser"], _configuration["ACDPass"]))
            {
                response.Success = true;

                response.Token = CreateToken(userModel);

                return response;
            }
            else
            {
                response.Success = false;
                response.Message = "Incorrect User Name or Password";
                return response;
            }

        }

        public async Task<ResponseAccount> AutenticateMCR(string userName, string password)
        {
            response = new ResponseAccount();
            if (AutenticateToAD(_configuration["DirPathMCR"], _configuration["domainMCR"], userName, password, _configuration["ACDUserMCR"], _configuration["ACDPassMCR"]))
            {
                DataTable dt=ChooseMethodBasedOnUsername(userName);
                if(dt != null && dt.Rows.Count > 0) {
                    CreateToken(loginTrader);
                    response.Success = true;
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.Message = "User Name not Found or Inactive";
                    return response;
                }

             
            }
            else
            {
                response.Success = false;
                response.Message = "Incorrect User Name or Password";
                return response;
            }

        }

   



        bool AutenticateToAD(string dirPath, string _domain, string userName, string pwd, string _adAdminUser, string _adAdminPass)
        {
            loginTrader = new LoginTrader();
            // GetByOrganizationalUnit("IT");
            string domain = _domain;
            string LDAP_Path = dirPath;
            string adAdminUser = _adAdminUser;
            string adAdminPass = _adAdminPass;

            if (string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(LDAP_Path))
                return false;
            string domainAndUsername = domain + "\\" + userName;

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

                        loginTrader.Uniqueidentifier = userADEntry.Guid;
                        loginTrader.Name = userADEntry.Username;
                        userModel.OrganizationalUnit = GetOrganizationalUnit(distinguishedName);
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


        public void CreateToken(LoginTrader loginTrader)
        {
            var str = System.Text.Json.JsonSerializer.Serialize(loginTrader);
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "UserName"),
                    new Claim(ClaimTypes.NameIdentifier, "UserId"),
                    new Claim(ClaimTypes.Role, "Admin"),
                };
            var httpContext = this._httpContext.HttpContext;
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);



            httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new Microsoft.AspNetCore.Authentication.AuthenticationProperties { IsPersistent = false });




            //var str = System.Text.Json.JsonSerializer.Serialize(loginTrader);
            //var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, str));
            //identity.AddClaim(new Claim(ClaimTypes.Name, loginTrader.UserName));
            //identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            //var principal = new ClaimsPrincipal(identity);

            //var httpContext = this._httpContext.HttpContext;

            //httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new Microsoft.AspNetCore.Authentication.AuthenticationProperties { IsPersistent = false });


            //Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignInAsync(httpContext, principal,
            //    new Microsoft.AspNetCore.Authentication.AuthenticationProperties { IsPersistent = false });


        }

        public static string Generate256BitKey()
        {
            byte[] key = new byte[32];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }

            return Convert.ToBase64String(key);
        }

        public static string GetMemberId()
        {
            byte[] key = new byte[32];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }

            return Convert.ToBase64String(key);
        }


        public DataTable ChooseMethodBasedOnUsername(string Username)
        {
            // Check if username is not null or empty
            if (string.IsNullOrEmpty(Username))
            {
                return null;
            }

            // Get the first character of the username
            char firstChar = Username[0];
            Username = Username.ToUpper();

            // Decide which method to call based on the first character of the username
            switch (firstChar)
            {
                case 'r':
                case 'R':
                   return GetRep(Username);
                    break;
                case 'm':
                case 'M':
                    return GetMeb(Username);
                    break;
                case 'c':
                case 'C':
                    return GetClient(Username);
                    break;
                default:
                    return new DataTable();
            }
        }

        public DataTable GetRep(string id)
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXStaggingConnectionStringMembership"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                SqlParameter sqlParameter = new SqlParameter("@IdNo", SqlDbType.NVarChar);
                sqlParameter.Value = id;
                sqlDataAdapter.SelectCommand.Parameters.Add(sqlParameter);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "GetRepDetailforwebsite";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state = ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }

            if (dt.Rows.Count > 0)
            {
                var memberId = dt.Rows[0]["memberId"];
               
                if (memberId != DBNull.Value)
                {
                    userModel.memberId = (Guid)memberId;
                }
                else
                {
                    // Handle case when memberId is null
                }
            }

            return dt;

        }

        public DataTable GetMeb(string id)
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXBUSAPINSTANCEConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "GetActiveRepByIDNo";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state = ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }
            return dt;
        }

        public DataTable GetClient(string id)
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXBUSAPINSTANCEConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "GetActiveRepByIDNo";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state = ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }
            return dt;
        }


        public static List<dynamic> MapDataTableToDynamicList(DataTable dataTable)
        {
            var list = new List<dynamic>();

            foreach (DataRow row in dataTable.Rows)
            {
                dynamic user = new ExpandoObject();

                // Add properties to the dynamic object based on the DataTable columns
                foreach (DataColumn column in dataTable.Columns)
                {
                    var dictionary = (IDictionary<string, object>)user;
                    dictionary[column.ColumnName] = row[column];
                }

                list.Add(user);
            }

            return list;
        }

        public DataTable GetActiveCommodities()
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXLookupConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "spGetCommodity";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state = ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }

            return dt;


        }

        //GetActiveRepByIDNo


    }
}
