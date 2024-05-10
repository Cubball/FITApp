using FITApp.Auth.Extensions;
using FITApp.EmployeesService;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
using FITApp.EmployeesService.Repositories;
using FITApp.EmployeesService.Services;
using FITApp.EmployeesService.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtPublicKey = builder.Configuration["JwtOptions:PublicKey"] ?? throw new InvalidOperationException("JwtOptions:PublicKey is not set");
builder.Services.AddJWTAuth(jwtPublicKey);

builder.Services.AddValidatorsFromAssemblyContaining<PositionDtoValidator>(ServiceLifetime.Transient);
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddSingleton<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddSingleton<IEmployeesService, EmployeesService>();
builder.Services.AddSingleton<IUsersService, UsersService>();
builder.Services.AddSingleton<IPhotoService, PhotoService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

MongoDbClassMapInitializer.RegisterClassMaps();
MongoDbClassMapInitializer.AddConventionPack();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();