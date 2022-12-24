/*using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class UserConfirmation : BaseEntity
{
    public string? Secret { get; set; }
    public string? Key { get; set; }
    public int UserId { get; set; }

    public virtual User User { get; set; }
}

*/

using camp_fire.Domain.Entities;

namespace camp_fire.Application.Models;

public class UserConfirmationResponseVM
{
    public int UserId { get; set; }
    public string Secret { get; set; }
    public string Key { get; set; }
    public User UserName { get; set; }
}
