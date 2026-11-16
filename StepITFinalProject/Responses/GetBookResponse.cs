namespace StepITFinalProject.Responses;

public class GetBookResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get;set; }
    public int PublicationYear { get; set; }
    public string AuthorName { get; set; }
    public List<string> Genres { get; set; } = new();
    public bool IsAvailable { get; set; }
}
