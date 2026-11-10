namespace StepITFinalProject.Requests;

public class CreateBookRequest
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }
    public int AvailableCopies { get; set; }
    public int AuthorId { get; set; }

}
