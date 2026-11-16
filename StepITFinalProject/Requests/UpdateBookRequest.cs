namespace StepITFinalProject.Requests;

public class UpdateBookRequest
{
    public string Title { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public int PublishedYear { get; set; }
    public int TotalCopies { get; set; }
    public int AuthorId { get; set; }
    public List<int> GenreIds { get; set; } = new();
}
