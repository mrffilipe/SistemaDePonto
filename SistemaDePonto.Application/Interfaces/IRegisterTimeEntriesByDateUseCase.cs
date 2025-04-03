using SistemaDePonto.Application.UseCases;

namespace SistemaDePonto.Application.Interfaces
{
    public interface IRegisterTimeEntriesByDateUseCase
    {
        Task<RegisterTimeEntryResponse> ExecuteAsync(Guid userId, RegisterTimeEntryRequest request);
    }
}
