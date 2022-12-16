namespace camp_fire.Application.Token;

public class JwtTokenModel
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? AccessToken { get; set; }
    public DateTime ExpiresIn { get; set; }
    public string? RefreshToken { get; set; }
}
