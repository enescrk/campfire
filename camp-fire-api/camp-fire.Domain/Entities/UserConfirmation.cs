using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class UserConfirmation : BaseEntity
{
    public string? Secret { get; set; }
    public string? Key { get; set; }
    public int UserId { get; set; }

    public virtual User User { get; set; }
}
