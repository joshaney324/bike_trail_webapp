using System.ComponentModel.DataAnnotations;

namespace bike_gps_crud.Models
{
    public class RegisterUser
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
    
    public class ViewTrail
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? Difficulty { get; init; }
        public string? ImageUrl { get; init; }
        public string? TrailType { get; init; }
        public string? GpxTrack { get; init; }
    }
}