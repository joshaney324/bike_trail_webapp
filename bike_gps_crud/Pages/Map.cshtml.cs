using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bike_gps_crud.Pages;

public class MapModel : PageModel
{
    public string MapboxAccessToken { get; } = "pk.eyJ1Ijoiam9zaGFuZXkzMjQiLCJhIjoiY204OTJtZDl3MHdxajJqcHMza2R2bHRyOCJ9.UgXb_yFl0DtJVaFrJ0qRxw";

    public void OnGet()
    {
    }
}