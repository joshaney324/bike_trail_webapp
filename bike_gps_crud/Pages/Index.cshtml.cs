using bike_gps_crud.Data;
using bike_gps_crud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace bike_gps_crud.Pages;

public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    public IList<Trail> Trails { get; set; }
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _logger = logger;
        _userManager = userManager;
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        

        if (user == null)
        {
            return Challenge(); // force login
        }

        var userId = user.Id;  // parse string ID into Guid

        Trails = await _context.Trail
            .Where(t => t.UserId == userId)
            .ToListAsync();

        return Page();
    }
    
}