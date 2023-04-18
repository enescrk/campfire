using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class Event : BaseEntity
{
    public string? Name { get; set; }// event adı
    public DateTime Date { get; set; } //event başlangıç tarihi
    public int[]? ParticipiantIds { get; set; }//event davetlileri id listesi kaldırılabilir
    public int? CompanyId { get; set; } // event'i oluşturan kullanıcının eklediği şirketId
    public int[]? PageIds { get; set; } //evente ait oyun id'leri kaldırılabilir
    public string? HashedKey { get; set; } //unique hashed kaldırılabilir
    public string? MeetingUrl { get; set; } // event'in url'i kaldırılabilir
    public int UserId { get; set; } //event'i oluşturan kullanıcı
    public int? CurrentPageId { get; set; } //şu an ekranda görünmesi gereken oyun
    public int? CurrentUserId { get; set; } //şu an oynaması gereken oyuncu
    public bool IsCompleted { get; set; }

    public virtual User? User { get; set; }
    public virtual ICollection<Page> Pages { get; set; }
    public virtual ICollection<Scoreboard> Scoreboards { get; set; }
    public virtual ICollection<EventParticipant> EventParticipants { get; set; }
}