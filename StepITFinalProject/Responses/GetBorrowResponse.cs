using Microsoft.VisualBasic;

namespace StepITFinalProject.Responses;

public class GetBorrowResponse
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public string ReaderName { get; set; }
    public DateTime BorrowedAt { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public bool IsOverdue { get; set; }
    public int DaysOverdue { get; set; }
}
