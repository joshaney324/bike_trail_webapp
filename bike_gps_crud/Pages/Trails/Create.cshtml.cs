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
        private readonly string storagePath;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
            // Store images in wwwroot/Trail-Images
            storagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Trail-Images");
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Trail Trail { get; set; } = null!;

        [BindProperty]
        public IFormFile TrailImage { get; set; } = null!;

        public string Message { get; set; } = "";

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (TrailImage == null || TrailImage.Length == 0)
            {
                Message = "No file selected.";
                return Page();
            }

            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }

            var fileName = $"{Guid.NewGuid()}.png";
            var filePath = Path.Combine(storagePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await TrailImage.CopyToAsync(stream);
            }

            Trail.ImageUrl = $"Trail-Images/{fileName}";

            _context.Trail.Add(Trail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
