namespace bike_gps_crud.Models;

public class UserTrail
{
    public int UserTrailId { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    public int TrailId { get; set; }
    public required Trail Trail { get; set; }
    public DateTime DateRidden { get; set; }
}