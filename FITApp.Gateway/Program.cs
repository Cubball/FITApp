using FITApp.Gateway.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms<RequestTransformProvider>()
    .AddTransforms<ResponseTransformProvider>();

var allowedOrigins = builder.Configuration.GetSection("CorsOptions:AllowedOrigins").Get<string[]>()
    ?? throw new InvalidOperationException("CorsOptions:AllowedOrigins is not set.");
builder.Services.AddCors(o =>
        o.AddDefaultPolicy(pb =>
            pb.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()));

var app = builder.Build();

app.UseCors();

app.MapReverseProxy();

app.Run();