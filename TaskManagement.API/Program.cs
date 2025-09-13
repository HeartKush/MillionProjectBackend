using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Application.Services;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Repositories;
using MongoDB.Driver;
using DotNetEnv;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables de entorno (no obligatorio en Docker, útil localmente)
Env.Load();

var mongoConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
var databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");

// Fallback a configuración (appsettings / variables heredadas)
mongoConnectionString = string.IsNullOrWhiteSpace(mongoConnectionString)
    ? builder.Configuration["MONGO_CONNECTION_STRING"]
    : mongoConnectionString;
databaseName = string.IsNullOrWhiteSpace(databaseName)
    ? builder.Configuration["DATABASE_NAME"]
    : databaseName;

if (string.IsNullOrWhiteSpace(mongoConnectionString) || string.IsNullOrWhiteSpace(databaseName))
{
    throw new InvalidOperationException("MONGO_CONNECTION_STRING o DATABASE_NAME no configurados. Define variables de entorno o config en appsettings.");
}

// Registrar servicios en el contenedor
builder.Services.AddSingleton<IMongoClient, MongoClient>(_ => new MongoClient(mongoConnectionString));
builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(databaseName);
});
builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IPropertyService, PropertyService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(_ => true);
    });
});

// Configuración de Swagger
builder.Services.AddSwaggerGen(options =>
{
    // Agregar documentación en XML
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configurar middlewares
app.UseSwagger(c =>
{
    c.PreSerializeFilters.Add((swagger, httpReq) =>
    {
        swagger.Servers = new List<Microsoft.OpenApi.Models.OpenApiServer>
        {
            new Microsoft.OpenApi.Models.OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" }
        };
    });
});
app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseCors("FrontendPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
