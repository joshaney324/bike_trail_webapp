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
    public class DetailsModel : PageModel
    {
        private readonly bike_gps_crud.Data.ApplicationDbContext _context;

        public DetailsModel(bike_gps_crud.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public UserTrail UserTrail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usertrail = await _context.UserTrail.FirstOrDefaultAsync(m => m.UserTrailId == id);
            if (usertrail == null)
            {
                return NotFound();
            }
            else
            {
                UserTrail = usertrail;
            }
            return Page();
        }
    }
}
