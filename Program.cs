using GameLibrary.DotNet.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var apiKey = builder.Configuration["Auth:ApiKey"];

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

    options.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Enter your API key.",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Name = "X-API-Key",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

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
