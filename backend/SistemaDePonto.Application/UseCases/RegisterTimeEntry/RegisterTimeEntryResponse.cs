using SistemaDePonto.Domain.Enums;

namespace SistemaDePonto.Application.UseCases
{
    public class RegisterTimeEntryResponse
    {
        public DateTime Timestamp { get; set; }
        public EntryType Type { get; set; }
        public string? Message { get; set; }
    }
}
