using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using bike_gps_crud.Data;
using bike_gps_crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace bike_gps_crud.Pages.Trails
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Trail> Trails { get; set; } = new List<Trail>();

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? TrailTypes { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? TrailType { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
            {
                return Challenge(); // force login
            }

            var userId = user.Id;
            
            
            IQueryable<string> trailTypeQuery = _context.Trail
                .Where(t => t.UserId == userId)
                .OrderBy(t => t.TrailType)
                .Select(t => t.TrailType);
            
            var trailsQuery = _context.Trail
                .Where(t => t.UserId == userId);
            
            if (!string.IsNullOrEmpty(SearchString))
            {
                trailsQuery = trailsQuery.Where(t => t.Name.Contains(SearchString));
            }
            
            if (!string.IsNullOrEmpty(TrailType))
            {
                trailsQuery = trailsQuery.Where(t => t.TrailType == TrailType);
            }

            TrailTypes = new SelectList(await trailTypeQuery.Distinct().ToListAsync());
            Trails = await trailsQuery.ToListAsync();
            
            return Page();
            
        }
    }
}