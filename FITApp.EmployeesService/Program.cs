using FITApp.Auth.Extensions;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
using FITApp.EmployeesService.Repositories;
using FITApp.EmployeesService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtPublicKey = builder.Configuration["JwtOptions:PublicKey"] ?? throw new InvalidOperationException("JwtOptions:PublicKey is not set");
builder.Services.AddJWTAuth(jwtPublicKey);


builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddSingleton<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddSingleton<IEmployeesService, EmployeesService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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