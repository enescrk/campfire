namespace camp_fire.Domain.SeedWork.Exceptions;

public class ApiException : Exception
{
    public ApiException(string message) : base(message)
    {

    }

    public ApiException(List<string> messages)
    : base(string.Join(" | ", messages))
    {

    }
}
