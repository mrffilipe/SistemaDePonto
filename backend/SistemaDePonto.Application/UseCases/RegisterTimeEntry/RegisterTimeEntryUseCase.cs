using SistemaDePonto.Application.Interfaces;
using SistemaDePonto.Domain.Entities;
using SistemaDePonto.Domain.Interfaces;

namespace SistemaDePonto.Application.UseCases
{
    public class RegisterTimeEntryUseCase : IRegisterTimeEntryUseCase
    {
        private readonly ITimeEntryRepository _repository;

        public RegisterTimeEntryUseCase(ITimeEntryRepository repository)
        {
            _repository = repository;
        }

        public async Task<RegisterTimeEntryResponse> ExecuteAsync(Guid userId, RegisterTimeEntryRequest request)
        {
            var today = DateTime.UtcNow.Date;
            var entriesToday = await _repository.GetByDateAsync(userId, today);

            if (entriesToday.Any(e => e.Type == request.Type))
            {
                throw new Exception("Você já registrou esse tipo de ponto hoje.");
            }

            var entry = new TimeEntry(userId, DateTime.UtcNow, request.Type);

            await _repository.AddAsync(entry);

            return new RegisterTimeEntryResponse
            {
                Timestamp = entry.Timestamp,
                Type = entry.Type,
                Message = "Registro realizado com sucesso."
            };
        }
    }
}
