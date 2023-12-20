using camp_fire.Domain.Entities.Base;

public class Subscribtion : BaseEntity
{
    public int? UserId { get; set; }
    public int? CompanyId { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public int? EnterpriceLevelId { get; set; }
}

