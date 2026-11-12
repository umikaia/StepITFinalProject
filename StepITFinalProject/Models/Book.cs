namespace StepITFinalProject.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }


    public int AuthorId { get; set; }
    public Author Author { get; set; }

    public List<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();

    public List<Genre> Genres { get; set; } = new List<Genre>();

}
