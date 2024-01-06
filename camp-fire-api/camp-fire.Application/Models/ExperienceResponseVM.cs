namespace camp_fire.Application.Models;

public class ExperienceResponse
{
    public int? Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public List<string> Categories { get; set; }
    public decimal? Price { get; set; }
    public string? Image { get; set; }
    public CurrencyType? Currency { get; set; }
    public string? BannerImage { get; set; }
    public int? Duration { get; set; }
    public string? VideoUrl { get; set; }
    public BoxResponseVM? Box { get; set; } //
    public string? Content { get; set; }
    public List<string>? Images { get; set; }
    public string? Header { get; set; }
    public string? HeaderContent { get; set; }
    public List<AgendaResponse>? Agendas { get; set; }
    public int? OwnerId { get; set; } //
    public int? EnterpriceLevelId { get; set; } //
}

public class GetExperienceRequest
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public List<string>? Categories { get; set; }
    public int? EnterpriceLevelId { get; set; } //
}

public class UpdateExperienceRequest : CreateExperienceRequest
{
    public int Id { get; set; }

}

public class CreateExperienceRequest
{
    public string Title { get; set; }
    public string Summary { get; set; }
    public List<string> Categories { get; set; }
    public decimal? Price { get; set; }
    public string? Image { get; set; }
    public CurrencyType? Currency { get; set; }
    public string? BannerImage { get; set; }
    public int? Duration { get; set; }
    public string? VideoUrl { get; set; }
    //public int? BoxID { get; set; } //
    public CreateBoxRequestVM? Box { get; set; }
    public string? Content { get; set; }
    public List<string>? Images { get; set; }
    public string? Header { get; set; }
    public string? HeaderContent { get; set; }
    public List<int>? AgendaIds { get; set; }
    public int? OwnerId { get; set; } //
    public int? EnterpriceLevelId { get; set; } //
}