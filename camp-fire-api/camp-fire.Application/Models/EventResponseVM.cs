using camp_fire.Domain.Entities;

namespace camp_fire.Application.Models;

public class EventResponseVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string HashedKey { get; set; }
    public User UserName { get; set; }
    public List<Scoreboard> Scoreboards { get; set; }
    public List<Page> Pages { get; set; }
}
