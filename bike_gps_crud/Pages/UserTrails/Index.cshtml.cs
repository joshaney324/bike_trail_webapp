using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using bike_gps_crud.Data;
using bike_gps_crud.Models;

namespace bike_gps_crud.Pages.UserTrails
{
    public class IndexModel : PageModel
    {
        private readonly bike_gps_crud.Data.ApplicationDbContext _context;

        public IndexModel(bike_gps_crud.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<UserTrail> UserTrail { get;set; } = default!;

        public async Task OnGetAsync()
        {
            UserTrail = await _context.UserTrail
                .Include(u => u.Trail)
                .Include(u => u.User).ToListAsync();
        }
    }
}
