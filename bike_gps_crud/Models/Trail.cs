using System.ComponentModel.DataAnnotations;
using bike_gps_crud.Models;
namespace bike_gps_crud.Models;

public class Trail
{
    public int Id { get; init; }
    [MaxLength(50)]
    public string? Name { get; init; }
    [MaxLength(250)]
    public string? Description { get; init; }
    [MaxLength(15)]
    public string? Difficulty { get; init; }
    [MaxLength(250)]
    public string? ImageUrl { get; set; }
    [MaxLength(20)]
    public string? TrailType { get; init; }
    [MaxLength(250)]
    public string? GpxTrack { get; set; } 
    public DateTime DateAdded { get; } = DateTime.Now;
    
    // Foreign Key
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}