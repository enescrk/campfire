namespace camp_fire.Application.Models;

public class BoxResponseVM
{
    public int Id { get; set; }
    public List<string> Images { get; set; }
    public string Description { get; set; }
}


public class GetBoxRequestVM
{
    public int? Id { get; set; }
    public List<int>? Ids { get; set; }
}

public class CreateBoxRequestVM
{
    public List<string> Images { get; set; }
    public string Description { get; set; }
}

public class UpdateBoxRequestVM : CreateBoxRequestVM
{
    public int Id { get; set; }
}


