using SistemaDePonto.Application.UseCases;

namespace SistemaDePonto.Application.Interfaces
{
    public interface IRegisterTimeEntryUseCase
    {
        Task<RegisterTimeEntryResponse> ExecuteAsync(Guid userId, RegisterTimeEntryRequest request);
    }
}
