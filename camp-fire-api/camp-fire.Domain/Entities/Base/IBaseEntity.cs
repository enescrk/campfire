namespace camp_fire.Domain.Entities.Base;

public interface IBaseEntity
{
    int Id { get; set; }
    int CreatedBy { get; set; }
    int UpdatedBy { get; set; }
    DateTime CreatedDate { get; set; }
    DateTime UpdatedDate { get; set; }
    bool IsDeleted { get; set; }
}
