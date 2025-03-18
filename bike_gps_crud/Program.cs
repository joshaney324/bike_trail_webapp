using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using bike_gps_crud.Data;
using Microsoft.AspNetCore.StaticFiles;
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

        // UNCOMMENT IF IN DEVELOPMENT (Removes database each build for easier testing)
        
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        
        
        var gpxTracksPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "gpx_tracks");
        var trailImagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Trail-Images");
        
        
        DeleteFilesInDirectory(gpxTracksPath);
        DeleteFilesInDirectory(trailImagesPath);
    }
}

void DeleteFilesInDirectory(string path)
{
    if (Directory.Exists(path))
    {
        var files = Directory.GetFiles(path);
        foreach (var file in files)
        {
            try
            {
                File.Delete(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete {file}: {ex.Message}");
            }
        }
    }
}


app.UseHttpsRedirection();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[ ".gpx" ] = "text/plain";

app.UseStaticFiles( new StaticFileOptions
{
    ContentTypeProvider = provider
} );

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

