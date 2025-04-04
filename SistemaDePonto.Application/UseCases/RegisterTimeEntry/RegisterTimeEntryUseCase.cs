using SistemaDePonto.Application.Interfaces;

namespace SistemaDePonto.Application.UseCases
{
    public class RegisterTimeEntryUseCase : IRegisterTimeEntryUseCase
    {
        public Task<RegisterTimeEntryResponse> ExecuteAsync(Guid userId, RegisterTimeEntryRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
