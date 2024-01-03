namespace camp_fire.Application.Models.Response;

public class CompanyResponse
{
    public int? Id { get; set; }
    public string ShortName { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? SubscribtionId { get; set; }
    public List<int> TeamIds { get; set; }
}