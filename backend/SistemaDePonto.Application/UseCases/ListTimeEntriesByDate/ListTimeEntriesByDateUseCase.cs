using SistemaDePonto.Application.Interfaces;
using SistemaDePonto.Domain.Enums;
using SistemaDePonto.Domain.Interfaces;

namespace SistemaDePonto.Application.UseCases
{
    public class ListTimeEntriesByDateUseCase : IListTimeEntriesByDateUseCase
    {
        private readonly ITimeEntryRepository _repository;

        public ListTimeEntriesByDateUseCase(ITimeEntryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ListTimeEntriesByDateResponse>> ExecuteAsync(Guid userId, ListTimeEntriesByDateRequest request)
        {
            var entries = await _repository.GetByDateAsync(userId, request.StartDate, request.EndDate);

            var groupedByDate = entries.OrderBy(e => e.Timestamp).GroupBy(e => DateOnly.FromDateTime(e.Timestamp));

            var result = new List<ListTimeEntriesByDateResponse>();

            foreach (var group in groupedByDate)
            {
                var entry = group.FirstOrDefault(e => e.Type == EntryType.Entry);
                var exit = group.FirstOrDefault(e => e.Type == EntryType.Exit);

                var workedHours = (entry != null && exit != null)
                ? exit.Timestamp - entry.Timestamp
                : throw new Exception("Não foi possível calcular as horas trabalhadas para esta data.");

                result.Add(new ListTimeEntriesByDateResponse
                {
                    Date = group.Key,
                    Entry = entry?.Timestamp,
                    Exit = exit?.Timestamp,
                    WorkedHours = workedHours,
                });

            }

            return result;
        }
    }
}
