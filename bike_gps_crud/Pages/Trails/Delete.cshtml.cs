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
    public class DeleteModel : PageModel
    {
        private readonly bike_gps_crud.Data.ApplicationDbContext _context;

        public DeleteModel(bike_gps_crud.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trail = await _context.Trail.FindAsync(id);
            if (trail != null)
            {
                Trail = trail;
                _context.Trail.Remove(Trail);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
