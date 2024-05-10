using FITApp.Auth.Extensions;
using FITApp.IdentityService.Data;
using FITApp.IdentityService.Entities;
using FITApp.IdentityService.Infrastructure;
using FITApp.IdentityService.Options;
using FITApp.IdentityService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("IdentityDefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddIdentityCore<User>(o =>
    {
        o.User.RequireUniqueEmail = true;

        o.Password.RequireDigit = false;
        o.Password.RequireLowercase = false;
        o.Password.RequireUppercase = false;
        o.Password.RequireNonAlphanumeric = false;
        o.Password.RequiredLength = 8;
    })
    .AddRoles<Role>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

var jwtPublicKey = builder.Configuration["JwtOptions:PublicKey"] ?? throw new InvalidOperationException("JwtOptions:PublicKey is not set");
var jwtPrivateKey = builder.Configuration["JwtOptions:PrivateKey"] ?? throw new InvalidOperationException("JwtOptions:PrivateKey is not set");
builder.Services.AddJWTAuth(jwtPublicKey);

builder.Services.Configure<FITAppOptions>(builder.Configuration.GetSection(FITAppOptions.SectionName));
builder.Services.AddSingleton<IClock, SystemClock>();
builder.Services.AddSingleton<ITokenService, TokenService>(s => new TokenService(jwtPrivateKey, s.GetRequiredService<IClock>()));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordGenerator, PasswordGenerator>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var employeeServiceBaseUrl = builder.Configuration["FITAppOptions:EmployeeServiceBaseUrl"] ?? throw new InvalidOperationException("FITAppOptions:EmployeeServiceBaseUrl is not set");
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddHeaderPropagation(o => o.Headers.Add(HeaderNames.Authorization));
builder.Services
    .AddHttpClient<IEmployeeService, EmployeeService>(o => o.BaseAddress = new Uri(employeeServiceBaseUrl))
    .AddHeaderPropagation();

var app = builder.Build();

using var scope = app.Services.CreateScope();
await DbInitializer.InitializeAsync(scope, app.Configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHeaderPropagation();

app.UseAuthorization();

app.MapControllers();

app.Run();