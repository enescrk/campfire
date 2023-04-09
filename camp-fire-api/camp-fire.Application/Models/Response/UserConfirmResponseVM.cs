namespace camp_fire.Application.Models.Response;

public class UserConfirmResponseVM
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? AccessToken { get; set; }
    public DateTime ExpiresIn { get; set; }
    public string? RefreshToken { get; set; }
    public bool IsManager { get; set; }
}