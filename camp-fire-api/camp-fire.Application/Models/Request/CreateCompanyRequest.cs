namespace camp_fire.Application.Models.Request;

public class CreateCompanyRequest
{
    public string ShortName { get; set; }
    public CreateAddressRequestVM? Address { get; set; }
}