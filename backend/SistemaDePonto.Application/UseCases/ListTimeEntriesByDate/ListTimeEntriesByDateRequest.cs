namespace SistemaDePonto.Application.UseCases
{
    public class ListTimeEntriesByDateRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
