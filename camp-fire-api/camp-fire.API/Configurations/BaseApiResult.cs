namespace camp_fire.API.Configurations;

public class BaseApiResult
{
    public bool IsSuccess { get; set; } = true;
    public string? Message { get; set; }
    public int Count { get; set; }
    public object? Data { get; set; }
}
