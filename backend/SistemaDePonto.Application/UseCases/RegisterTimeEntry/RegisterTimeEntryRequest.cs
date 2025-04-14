using SistemaDePonto.Domain.Enums;

namespace SistemaDePonto.Application.UseCases
{
    public class RegisterTimeEntryRequest
    {
        public EntryType Type { get; set; }
    }
}
