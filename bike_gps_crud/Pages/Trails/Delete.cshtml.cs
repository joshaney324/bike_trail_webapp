using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting; 
using bike_gps_crud.Data;
using bike_gps_crud.Models;

namespace bike_gps_crud.Pages.Trails
{
    public class DeleteModel : PageModel
    {
        private readonly bike_gps_crud.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env; 

        public DeleteModel(bike_gps_crud.Data.ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env; 
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

                
                var imagePath = Path.Combine(_env.WebRootPath, Trail.ImageUrl);
                var gpxPath = Path.Combine(_env.WebRootPath, Trail.GpxTrack);
                
                if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                {
                    try
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting image file: {ex.Message}");
                    }
                }
                
                if (!string.IsNullOrEmpty(gpxPath) && System.IO.File.Exists(gpxPath))
                {
                    try
                    {
                        System.IO.File.Delete(gpxPath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting GPX file: {ex.Message}");
                    }
                }

                _context.Trail.Remove(trail);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
