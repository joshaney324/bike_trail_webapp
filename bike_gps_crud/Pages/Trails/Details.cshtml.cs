using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using bike_gps_crud.Data;
using bike_gps_crud.Models;

namespace bike_gps_crud.Pages.Trails
{
    public class DetailsModel : PageModel
    {
        private readonly bike_gps_crud.Data.ApplicationDbContext _context;

        public DetailsModel(bike_gps_crud.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Trail Trail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trail = await _context.Trail.FirstOrDefaultAsync(m => m.Id == id);
            if (trail == null)
            {
                return NotFound();
            }
            else
            {
                Trail = trail;
            }
            return Page();
        }
    }
}
