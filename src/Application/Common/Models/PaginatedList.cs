namespace QuizHub.Application.Common.Models;

public class PaginatedList<T>
{
    public required ICollection<T> Items { get; set; }
    public required int Count { get; init; }
}
