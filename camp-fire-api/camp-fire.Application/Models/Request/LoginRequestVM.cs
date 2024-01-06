namespace camp_fire.Application.Models.Request;

public class LoginRequestVM
{
    public string Email { get; set; }
    // public int? EventId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? Gender { get; set; }
}