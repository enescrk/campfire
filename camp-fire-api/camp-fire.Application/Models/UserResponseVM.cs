using camp_fire.Domain.Enums;

namespace camp_fire.Application.Models;

public class UserResponseVM
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int[]? AuthorizedCompanies { get; set; }
    public bool Gender { get; set; } //* male=true female=false
    public string? EMail { get; set; }
    public UserType UserType { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
}

public class ActiveUserResponseVM
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}

public class GetUserRequestVM
{
    public int? Id { get; set; }
    public List<int>? Ids { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int[]? AuthorizedCompanies { get; set; }
    public bool Gender { get; set; } //* male=true female=false
    public string? EMail { get; set; }
    public UserType UserType { get; set; }
    public string? PhoneNumber { get; set; }
}

public class UpdateUserRequestVM : UserResponseVM
{
}

public class CreateUserRequestVM : UserResponseVM
{
}
