namespace SistemaDePonto.Application.UseCases
{
    public class ListTimeEntriesByDateResponse
    {
        public DateOnly Date { get; set; }
        public DateTime? Entry { get; set; }
        public DateTime? Exit { get; set; }
        public TimeSpan? WorkedHours { get; set; }
    }
}
