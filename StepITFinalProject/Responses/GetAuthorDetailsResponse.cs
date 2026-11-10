namespace StepITFinalProject.Responses;

public class GetAuthorDetailsResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int BirthYear { get; set; }
    public string Country { get; set; }

    public List<AuthorBooks> Books { get; set; } = new List<AuthorBooks>();
}

public class AuthorBooks
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public string Genre { get; set; }
}
