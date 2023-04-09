namespace camp_fire.Application.Models.Request;

public class UserConfirmRequestVM
{
    public string Code { get; set; }
    public string Key { get; set; }
    public string Email { get; set; }
}