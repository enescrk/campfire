using camp_fire.Domain.Enums;

namespace camp_fire.Application.Models.Request;

public class GetGameRequestVM
{
    public List<int>? Ids { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public GameType Type { get; set; }
}

public class CreateGameRequestVM
{
    public string Name { get; set; }
    public string Description { get; set; }
    public GameType Type { get; set; }
}

public class UpdateGameRequestVM : CreateGameRequestVM
{
    public int Id { get; set; }
}