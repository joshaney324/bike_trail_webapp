using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bike_gps_crud.Data;
using bike_gps_crud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bike_gps_crud.Pages;

public class MapModel : PageModel
{
    public string MapboxAccessToken { get; }

    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public IList<Trail> Trails { get; set; } = new List<Trail>();

    public MapModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
        
        // Load MapboxAccessToken from mapboxsettings.json
        var json = System.IO.File.ReadAllText("mapboxsettings.json");
        var config = JsonSerializer.Deserialize<MapboxConfig>(json);
        MapboxAccessToken = config?.MapboxAccessToken ?? "";
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        

        if (user == null)
        {
            return Challenge(); // force login
        }

        var userId = user.Id;

        Trails = await _context.Trail
            .Where(t => t.UserId == userId)
            .ToListAsync();
        return Page();
    }
}

public class MapboxConfig
{
    public string MapboxAccessToken { get; set; }
}