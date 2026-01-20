using DotNetEnv;
using Duckov.Api.Categories;
using Duckov.Api.Extensions;
using Duckov.Api.Handlers;
using Duckov.Api.Items;
using Duckov.Api.Logins;

var builder = WebApplication.CreateBuilder(args);

// Environment
Env.Load(".env");
builder.Configuration.AddEnvironmentVariables();

// Core framework services
builder.Services.AddControllers();
builder.Services.AddSwagger();
builder.Services.AddOptions(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration, builder.Environment);

// App dependencies
builder.Services.AddItemsDependencies();
builder.Services.AddCategoriesDependencies();
builder.Services.AddLoginsDependencies();
builder.Services.AddExceptionHandlers();

var app = builder.Build();

// Middleware
app.UseSecurity();
app.UseSwaggerIfDevelopment();
app.UseExceptionHandler();
app.MapControllers();

app.Run();