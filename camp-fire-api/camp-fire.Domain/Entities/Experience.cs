using System.ComponentModel.DataAnnotations.Schema;
using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class Experience : BaseEntity
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
    public int? BoxId { get; set; } //
    public string? Content { get; set; }
    public List<string>? Images { get; set; }
    public string? Header { get; set; }
    public string? HeaderContent { get; set; }
    public List<int>? AgendaIds { get; set; }
    public int? OwnerId { get; set; } //
    public int? EnterpriceLevelId { get; set; } //
}