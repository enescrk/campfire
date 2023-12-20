using camp_fire.Domain.Entities.Base;

public class Company : BaseEntity
{
    public string ShortName { get; set; }
    public int AddressId { get; set; }
    public int SubscribtionId { get; set; }
    public List<int> TeamIds { get; set; }
}