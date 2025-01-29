using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using bike_gps_crud.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("bike_gps_crudContext") ?? throw new InvalidOperationException("Connection string 'bike_gps_crudContext' not found.")));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        // Get the database context
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Drop and recreate the database during development
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

