using GameLibrary.DotNet.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Session 9 practice:
// API key auth was tested with user-secrets and Swagger authorization.
// Disabled for now so the local React frontend can use the API without auth.

// Get the local API key from user-secrets.
// This keeps the key out of GitHub and away from the source code.
// var apiKey = builder.Configuration["Auth:ApiKey"];

// If the key is missing, stop the app early so we know what went wrong.
// if (string.IsNullOrWhiteSpace(apiKey))
// {
//     throw new Exception("Auth:ApiKey is missing. Add it with dotnet user-secrets.");
// }

builder.Services.AddDbContext<GameLibraryContext>(options =>
    options.UseSqlite("Data Source=gameLibrary.db"));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();

    // Session 9 practice:
    // Adds an API key option to Swagger.
    // This creates the Authorize button and tells Swagger to send X-API-Key in the request header.
    // Disabled for now because auth is not needed for the local React learning project.

    // options.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    // {
    //     Description = "Enter your API key.",
    //     Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
    //     Name = "X-API-Key",
    //     In = Microsoft.OpenApi.Models.ParameterLocation.Header,
    //     Scheme = "ApiKeyScheme"
    // });

    // Tells Swagger to apply the API key security option to the endpoints.

    // options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    // {
    //     {
    //         new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    //         {
    //             Reference = new Microsoft.OpenApi.Models.OpenApiReference
    //             {
    //                 Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
    //                 Id = "ApiKey"
    //             }
    //         },
    //         new string[] {}
    //     }
    // });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Session 9 practice:
// Simple API key middleware.
// GET requests stayed public.
// POST, PUT, and DELETE required the X-API-Key header to match the local user-secret.
// Disabled for now so the local React frontend can use the full API without auth.

// app.Use(async (context, next) =>
// {
//     var method = context.Request.Method;

//     if (method == "POST" || method == "PUT" || method == "DELETE")
//     {
//         var submittedApiKey = context.Request.Headers["X-API-Key"].FirstOrDefault();

//         if (submittedApiKey != apiKey)
//         {
//             context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//             await context.Response.WriteAsync("Invalid or missing API key.");
//             return;
//         }
//     }

//     await next();
// });

app.UseAuthorization();

app.MapControllers();

app.Run();