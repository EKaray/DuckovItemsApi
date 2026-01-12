using DuckovItemsApi.Categories;
using DuckovItemsApi.Data;
using DuckovItemsApi.Handlers;
using DuckovItemsApi.Items;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddItemsDependencies();
builder.Services.AddCategoriesDependencies();

builder.Services.AddExceptionHandlers();

builder.Services.AddDbContext<DuckovDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    if (builder.Environment.IsDevelopment())
    {
        var folderPath = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
        Directory.CreateDirectory(folderPath); // Ensure folder exists
    }

    options.UseSqlite(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DuckovDbContext>();
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();

app.MapControllers();

app.Run();
