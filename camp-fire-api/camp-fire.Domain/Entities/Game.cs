using camp_fire.Domain.Entities.Base;
using camp_fire.Domain.Enums;

namespace camp_fire.Domain.Entities;

public class Game : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public GameType Type { get; set; }

    public virtual ICollection<Page> Pages { get; set; }
}