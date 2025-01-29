namespace bike_gps_crud.Models;

public class User
{
    public int UserId { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public DateTime DateJoined { get; set; }

    public ICollection<UserTrail>? UserTrails { get; set; }
}