namespace camp_fire.Application.Models;

public class AgendaResponseVM
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
}

public class GetAgendaRequestVM
{
    public int? Id { get; set; }
    public List<int>? Ids { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Duration { get; set; }
}

public class CreateAgendaRequestVM
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public int? Duration { get; set; }
}

public class UpdateAgendaRequestVM : CreateAgendaRequestVM
{
    public int Id { get; set; }
}