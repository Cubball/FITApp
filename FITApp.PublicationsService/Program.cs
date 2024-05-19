using FITApp.PublicationsService.Interfaces;
using FITApp.PublicationsService.Repositories;
using FITApp.PublicationsService.Services;
using Microsoft.Net.Http.Headers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
MongoClient client = new MongoClient(builder.Configuration["MongoSettings:ConnectionString"]);
var database = client.GetDatabase(builder.Configuration["MongoSettings:DatabaseName"]);
builder.Services.AddSingleton(database);
builder.Services.AddScoped<IPublicationRepository, PublicationRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPublicationsService, PublicationsService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddHeaderPropagation(o => o.Headers.Add(HeaderNames.Authorization));
builder.Services.AddHttpClient<IPublicationsService, PublicationsService>(o => o.BaseAddress = new Uri(builder.Configuration["EmployeesServiceUrl"]))
    .AddHeaderPropagation();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();