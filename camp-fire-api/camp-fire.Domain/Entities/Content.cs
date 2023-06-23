using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class Content : BaseEntity
{
    public int EventId { get; set; }
    public string Data { get; set; }
}


// public class DataVM
// {
//     public int EventId { get; set; }
//     public string Story { get; set; }
// }
