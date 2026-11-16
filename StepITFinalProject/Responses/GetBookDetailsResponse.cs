namespace StepITFinalProject.Responses;

public class GetBookDetailsResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public int PublishedYear { get; set; }
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
    public bool IsAvailable { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
    public List<string> Genres { get; set; } = new();
}
