using App.Application.Contracts.Caching;
using App.Application.Extensions;
using App.Caching;
using App.Persistence.Extensions;
using CApp.API.ExceptionHandlers;
using CApp.API.Extensions;
using CApp.API.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllerWithFiltersExt().AddSwaggerExt().AddExceptionHandlerExt().AddCachingExt();

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

var app = builder.Build();

app.UseConfigurePipelineExt();

app.MapControllers();

app.Run();
