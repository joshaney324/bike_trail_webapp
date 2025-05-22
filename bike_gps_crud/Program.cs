using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using bike_gps_crud.Data;
using bike_gps_crud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using NoOpEmailSender = bike_gps_crud.Services.NoOpEmailSender;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("bike_gps_crudContext") ?? throw new InvalidOperationException("Connection string 'bike_gps_crudContext' not found.")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    
});

builder.Services.AddTransient<IEmailSender, NoOpEmailSender>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        
        // dbContext.Database.Migrate(); 

        // SEED A USER WITH ID = 1
        // if (!dbContext.User.Any(u => u.UserId == 1))
        // {
        //     dbContext.User.Add(new bike_gps_crud.Models.User
        //     {
        //         UserId = 1,
        //         Username = "testuser",
        //         PasswordHash = "placeholder",
        //         DateJoined = DateTime.Now
        //     });
        //
        //     dbContext.SaveChanges();
        // }

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

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

