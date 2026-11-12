namespace StepITFinalProject.Models;

public class LibraryCard
{
    public int Id { get; set; }
    public string CardNumber { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}