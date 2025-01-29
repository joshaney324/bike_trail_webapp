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
    public class DeleteModel : PageModel
    {
        private readonly bike_gps_crud.Data.ApplicationDbContext _context;

        public DeleteModel(bike_gps_crud.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usertrail = await _context.UserTrail.FindAsync(id);
            if (usertrail != null)
            {
                UserTrail = usertrail;
                _context.UserTrail.Remove(UserTrail);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
