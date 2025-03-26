using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bike_gps_crud.Data;
using bike_gps_crud.Models;

namespace bike_gps_crud.Pages.Trails
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly string _imageStoragePath;
        private readonly string _gpxStoragePath;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
            // Store images in wwwroot/Trail-Images
            _imageStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Trail-Images");
            _gpxStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "gpx_tracks");
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Trail Trail { get; set; } = null!;

        [BindProperty]
        public IFormFile TrailImage { get; set; } = null!;
        [BindProperty]
        public IFormFile Gpx { get; set; } = null!;

        public string Message { get; set; } = "";

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            
            // Validate Image Upload
            if (TrailImage == null || TrailImage.Length == 0)
            {
                Message = "No image file selected.";
                return Page();
            }

            if (!Directory.Exists(_imageStoragePath))
            {
                Directory.CreateDirectory(_imageStoragePath);
            }

            var imageFileName = $"{Guid.NewGuid()}.png";
            var imageFilePath = Path.Combine(_imageStoragePath, imageFileName);

            using (var stream = new FileStream(imageFilePath, FileMode.Create))
            {
                await TrailImage.CopyToAsync(stream);
            }

            Trail.ImageUrl = $"Trail-Images/{imageFileName}";  // Correct image path

            // Validate GPX Upload
            if (Gpx == null || Gpx.Length == 0)
            {
                Message = "No GPX file selected.";
                return Page();
            }

            if (!Directory.Exists(_gpxStoragePath))
            {
                Directory.CreateDirectory(_gpxStoragePath);
            }

            var gpxFileName = $"{Guid.NewGuid()}.gpx";
            var gpxFilePath = Path.Combine(_gpxStoragePath, gpxFileName);

            using (var stream = new FileStream(gpxFilePath, FileMode.Create))
            {
                await Gpx.CopyToAsync(stream);
            }

            Trail.GpxTrack = $"gpx_tracks/{gpxFileName}";  // Correct GPX path
            
            _context.Trail.Add(Trail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
