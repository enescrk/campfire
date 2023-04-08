using camp_fire.Domain.Enums;

namespace camp_fire.Application.Models.Response;

public class GameResponseVM
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public GameType Type { get; set; }
}