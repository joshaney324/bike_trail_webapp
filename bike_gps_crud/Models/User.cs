using System.ComponentModel.DataAnnotations;

namespace bike_gps_crud.Models;

public class User
{
    public int UserId { get; set; }
    [Required]
    public string? Username { get; set; }
    public string? Email { get; set; }
    [Required]
    public string? PasswordHash { get; set; }
    public DateTime DateJoined { get; set; }

    public ICollection<Trail>? Trails { get; set; } = new List<Trail>();
}