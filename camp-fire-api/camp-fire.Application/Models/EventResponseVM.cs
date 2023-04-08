namespace camp_fire.Application.Models;

public class EventResponseVM
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? HashedKey { get; set; }
    public DateTime Date { get; set; }
    public UserResponseVM? User { get; set; }
    public List<ScoreboardResponseVM>? Scoreboards { get; set; }
    public List<int>? PageIds { get; set; }
    public List<int>? ParticipiantIds { get; set; }
    public int? CurrentPageId { get; set; }
    public int? CurrentUserId { get; set; }
    public List<PageResponseVM> Pages { get; set; }
    public List<UserResponseVM> Users { get; set; }
}

public class UpdateEventRequestVM
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? HashedKey { get; set; }
    public DateTime Date { get; set; }
    public int[]? ParticipiantIds { get; set; }
    public int? CompanyId { get; set; }
    public List<int> GameIds { get; set; }
    public string? MeetingUrl { get; set; }
    public int? CurrentPageId { get; set; }
}

public class CreateEventReqeustVM : UpdateEventRequestVM
{
    public List<CreateUserRequestVM> ParticipiantUsers { get; set; }
}

public class ChangeTurnRequestVM
{
    public int EventId { get; set; }
}

public class UpdatePageRequestVM
{
    public int EventId { get; set; }
    public int PageId { get; set; }
}

public class UpdateActiveUserRequestVM
{
    public int EventId { get; set; }
    public int UserId { get; set; }
}

public class PageVM
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public bool IsComleted { get; set; }
}