using GameLibrary.DotNet.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Get the local API key from user-secrets.
// This keeps the key out of GitHub and away from the source code.
var apiKey = builder.Configuration["Auth:ApiKey"];

// If the key is missing, stop the app early so we know what went wrong.
if (string.IsNullOrWhiteSpace(apiKey))
{
    throw new Exception("Auth:ApiKey is missing. Add it with dotnet user-secrets.");
}

builder.Services.AddDbContext<GameLibraryContext>(options =>
    options.UseSqlite("Data Source=gameLibrary.db"));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();

    // Adds an API key option to Swagger.
    // This creates the Authorize button and tells Swagger to send X-API-Key in the request header.
    options.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Enter your API key.",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Name = "X-API-Key",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    // Tells Swagger to apply the API key security option to the endpoints.
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Simple API key middleware.
// GET requests stay public.
// POST, PUT, and DELETE require the X-API-Key header to match the local user-secret.
app.Use(async (context, next) =>
{
    var method = context.Request.Method;

    if (method == "POST" || method == "PUT" || method == "DELETE")
    {
        var submittedApiKey = context.Request.Headers["X-API-Key"].FirstOrDefault();

        if (submittedApiKey != apiKey)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid or missing API key.");
            return;
        }
    }

    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();