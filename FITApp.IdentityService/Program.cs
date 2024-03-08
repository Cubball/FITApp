using FITApp.Auth;
using FITApp.IdentityService.Data;
using FITApp.IdentityService.Entities;
using FITApp.IdentityService.Infrastructure;
using FITApp.IdentityService.Options;
using FITApp.IdentityService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// NOTE: for now, we only have an SQLite database for development, we'll add another one later
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app.db"));
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
    .AddEntityFrameworkStores<AppDbContext>();

// TODO: find a better way to load keys
var jwtOptionsSection = builder.Configuration.GetSection(JwtOptions.SectionKey);
var jwtOptions = jwtOptionsSection.Get<JwtOptions>()!;
builder.Services.AddJWTAuth(jwtOptions.PublicKey);
builder.Services.Configure<JwtOptions>(jwtOptionsSection);
builder.Services.AddSingleton<IClock, SystemClock>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
await DbInitializer.InitializeAsync(scope, app.Configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();