using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using bike_gps_crud.Data;
using bike_gps_crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace bike_gps_crud.Pages.Trails
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Trail> Trails { get; set; } = new List<Trail>();

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? TrailTypes { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? TrailType { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<string> trailTypeQuery = from m in _context.Trail
                orderby m.TrailType
                select m.TrailType;
            
            var trailsQuery = from t in _context.Trail
                select t;
            
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
            
            
        }
    }
}