namespace Worker.API.Controllers.Responses;

public class JobResponse
{
    public DateOnly Date { get; set; }
    public string Name { get; set; } = string.Empty;
}
