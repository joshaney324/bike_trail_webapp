using System.ComponentModel.DataAnnotations;
using bike_gps_crud.Models;
namespace bike_gps_crud.Models;



public class Trail
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Difficulty { get; set; }
    public string? ImageUrl { get; set; }
    public string? TrailType { get; set; }
    public string GpxTrack { get; set; } 
    public DateTime DateAdded { get; set; } 
}