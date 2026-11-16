using AutoMapper;
using StepITFinalProject.Models;
using StepITFinalProject.Responses;

namespace StepITFinalProject.Profiles;

public class BorrowProfile : Profile
{
    public BorrowProfile()
    {
        CreateMap<BorrowRecord, GetBorrowResponse>()
            .ForMember(d => d.BookTitle, o => o.MapFrom(s => s.Book.Title))
            .ForMember(d => d.ReaderName, o => o.MapFrom(s => s.User.FullName))
            .ForMember(d => d.IsOverdue, o => o.MapFrom(s => s.ReturnedAt == null && s.DueDate < DateTime.UtcNow))
            .ForMember(d => d.DaysOverdue, o => o.MapFrom(s =>
                (s.ReturnedAt == null && s.DueDate < DateTime.UtcNow)
                    ? (int)Math.Ceiling((DateTime.UtcNow - s.DueDate).TotalDays)
                    : 0));
    }
}
