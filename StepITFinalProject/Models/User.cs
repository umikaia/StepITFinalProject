using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Runtime.InteropServices.Marshalling;

namespace StepITFinalProject.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }

    public LibraryCard LibraryCard { get; set; }
    public List<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();

}
