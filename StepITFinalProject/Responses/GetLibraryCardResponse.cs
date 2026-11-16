namespace StepITFinalProject.Responses;

public class GetLibraryCardResponse
{
    public string CardNumber { get; set; } = null!;
    public DateTime IssuedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsActive { get; set; }
}
