using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using bike_gps_crud.Data;
using bike_gps_crud.Models;

namespace bike_gps_crud.Pages.UserTrails
{
    public class CreateModel : PageModel
    {
        private readonly bike_gps_crud.Data.ApplicationDbContext _context;

        public CreateModel(bike_gps_crud.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["TrailId"] = new SelectList(_context.Trail, "Id", "Id");
        ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");
            return Page();
        }

        [BindProperty]
        public UserTrail UserTrail { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.UserTrail.Add(UserTrail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
