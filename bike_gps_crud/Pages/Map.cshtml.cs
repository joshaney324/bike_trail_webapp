using Microsoft.AspNetCore.Mvc.RazorPages;
using bike_gps_crud.Data;
using bike_gps_crud.Models;
using Microsoft.EntityFrameworkCore;

namespace bike_gps_crud.Pages;

public class MapModel : PageModel
{
    public string MapboxAccessToken { get; } =
        "pk.eyJ1Ijoiam9zaGFuZXkzMjQiLCJhIjoiY204OTJtZDl3MHdxajJqcHMza2R2bHRyOCJ9.UgXb_yFl0DtJVaFrJ0qRxw";

    private readonly ApplicationDbContext _context;

    public IList<Trail> Trails { get; set; } = new List<Trail>();

    public MapModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        var trailsQuery = from t in _context.Trail
            select t;
        
        Trails = await trailsQuery.ToListAsync();
    }
}