using ContactManager.Application.Extensions;
using ContactManager.Extensions;
using ContactManager.Features.Helpers;
using ContactManager.Features.Utilities;
using ContactManager.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Host.ConfigureSerilog();

builder.Services
    .AddApplicationLayer()
    .AddInfrastructureLayer(config)
    .AddAPILayer(config);

var app = builder.Build();

await app.SetUpDatabaseAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseSerilogRequestLogging();

app.UseCors(AppPolicies.CorsPolicy);

EndpointMapper.MapAllEndpoints(app);

app.Run();