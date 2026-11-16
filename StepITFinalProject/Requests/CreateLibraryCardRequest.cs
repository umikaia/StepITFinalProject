namespace StepITFinalProject.Requests;

public class CreateLibraryCardRequest
{
    public string CardNumber { get; set; }
    public DateTime IssuedAt { get; set; }
    public DateTime ExpirationDate { get; set; }
}
