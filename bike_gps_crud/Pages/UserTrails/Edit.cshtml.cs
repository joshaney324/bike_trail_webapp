using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bike_gps_crud.Data;
using bike_gps_crud.Models;

namespace bike_gps_crud.Pages.UserTrails
{
    public class EditModel : PageModel
    {
        private readonly bike_gps_crud.Data.ApplicationDbContext _context;

        public EditModel(bike_gps_crud.Data.ApplicationDbContext context)
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

            var usertrail =  await _context.UserTrail.FirstOrDefaultAsync(m => m.UserTrailId == id);
            if (usertrail == null)
            {
                return NotFound();
            }
            UserTrail = usertrail;
           ViewData["TrailId"] = new SelectList(_context.Trail, "Id", "Id");
           ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserTrail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTrailExists(UserTrail.UserTrailId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserTrailExists(int id)
        {
            return _context.UserTrail.Any(e => e.UserTrailId == id);
        }
    }
}
