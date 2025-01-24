using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using bike_gps_crud.Data;
using bike_gps_crud.Models;

namespace bike_gps_crud.Pages.Trails
{
    public class IndexModel(ApplicationDbContext context) : PageModel
    {
        public IList<Trail> Trails { get;set; } = null!;

        public async Task OnGetAsync()
        {
            Trails = await context.Trail.ToListAsync();
        }
    }
}
