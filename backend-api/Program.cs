using System.Text.Json;
using System.Text.Json.Serialization;
using acadamy_api.Interceptors;
using backend_api.Context;
using backend_api.Repositories;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

//Adding the connection string sql
builder.Services.AddDbContextFactory<MasterDbContext>(options =>
    options.UseSqlServer());

//Adding the camelcase json
builder.Services.Configure<JsonSerializerOptions>(jsonSerializerOptions =>
{
    jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    jsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    jsonSerializerOptions.PropertyNameCaseInsensitive = false;
});

builder.Services.AddSingleton<RemoveAliasInterceptor>();

//Adding the interface of car repository
builder.Services.AddSingleton<ICarRepository, CarRepository>();
builder.Services.AddSingleton<ICPersonRepository, PersonRepository>();

builder.Build().Run();