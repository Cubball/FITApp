using FITApp.Gateway.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms<RequestTransformProvider>()
    .AddTransforms<ResponseTransformProvider>();

var app = builder.Build();

app.MapReverseProxy();

app.Run();