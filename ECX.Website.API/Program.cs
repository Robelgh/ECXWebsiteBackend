using ECX.Website.Application;
using ECX.Website.Infrastructure;
using ECX.Website.Persistence;
using ECX.Website.Persistence.Repositories;
using miniOrange.saml;
using System.Reflection;
using Microsoft.AspNetCore.DataProtection;

using ECX.Website.Domain;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var _AppSettings = builder.Configuration.GetSection("AppSettings");
var _SecuritySetting = builder.Configuration.GetSection("SecuritySetting");
var _ConnectionStrings = builder.Configuration.GetSection("ConnectionStrings");

RaindropSessionStore sessionStore = new();
builder.Services.AddSingleton(sessionStore);

// Add services to the container.

builder.Services.AddSingleton<CountService>();
builder.Services.AddCors();
builder.Services.AddSignalR();
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddminiOrangeServices(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<ExternalApiService>();

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = _ConnectionStrings.GetValue<string>("identity_server");
    options.SchemaName = "dbo";
    options.TableName = "tbl_identity_cache";
});

builder.Services.AddAuthentication("Identity.Application").AddCookie("Identity.Application", options =>
{
    options.Cookie.Name = ".AspNet.RaindropSharedTraderInterface.Cookie";
    options.ExpireTimeSpan = System.TimeSpan.FromMinutes(Convert.ToDouble(_AppSettings.GetValue<string>("SessionTimeout")));
    options.LoginPath = "/Account/Login";
    options.Cookie.Domain = _AppSettings.GetValue<string>("Domain");
    options.Cookie.Path = "/";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.SlidingExpiration = true;
    IDataProtectionProvider d = DataProtectionProvider.Create(new DirectoryInfo(_AppSettings.GetValue<string>("DataProtectionPath")));
    options.DataProtectionProvider = d;
    options.SessionStore = sessionStore;
});
builder.Services.AddHttpClient("ExternalApi", client =>
{
client.BaseAddress = new Uri("https://mfa.ecx.com.et/"); // Base URL of the external API
client.DefaultRequestHeaders.Add("Accept", "application/json");
client.DefaultRequestHeaders.Add("Customer-Key", "1");
client.DefaultRequestHeaders.Add("Timestamp",
        "1741849537454");
client.DefaultRequestHeaders.Add("Authorization", "11df8551bd077431fac8795a96dfb83f9d6fb6fe8736fa689faef3448469fb00a6f01c6ecd57ccb72fd73cc4fb52bf1b537c4355e808d90598598fefe01e0054"); // Optional: Add headers
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            //policy.WithOrigins("http://10.3.5.95:17023")
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
});
// Add authorization

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//app.UseSwagger();
// app.UseSwaggerUI();
//}


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Enables detailed error pages in development
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

sessionStore.cache = (IDistributedCache)app.Services.GetService(typeof(IDistributedCache));
sessionStore.SessionTimeout = Convert.ToDouble(_AppSettings.GetValue<string>("SessionTimeout"));

//app.UseCors(builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecx public service"); c.RoutePrefix = string.Empty; });
app.UseCors("AllowSpecificOrigin");
app.UseRouting();
//app.UseAuthentication();
app.UseAuthorization();
//app.UseHttpsRedirection();
app.UseCookiePolicy();

//app.UseminiOrangeSAMLSSOMiddleware();


var countService = app.Services.GetRequiredService<CountService>();
countService.StartCounting();

app.MapControllers();
app.MapHub<SignalRHub>("/hub");

app.Run();