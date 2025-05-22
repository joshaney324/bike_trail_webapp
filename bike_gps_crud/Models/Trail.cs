using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bike_gps_crud.Models;
using Microsoft.AspNetCore.Identity;

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
    public required Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; } = null!;
}