namespace camp_fire.Domain.Entities.Base;

public abstract class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}
