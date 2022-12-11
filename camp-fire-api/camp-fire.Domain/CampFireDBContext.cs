using Microsoft.EntityFrameworkCore;

namespace camp_fire.Domain;

public class CampFireDBContext : DbContext, IDisposable, ICampFireDBContext
{

}
