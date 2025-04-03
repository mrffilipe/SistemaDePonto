using SistemaDePonto.Application.UseCases;

namespace SistemaDePonto.Application.Interfaces
{
    public interface IListTimeEntriesByDateUseCase
    {
        Task<List<ListTimeEntriesByDateResponse>> ExecuteAsync(Guid userId, ListTimeEntriesByDateRequest request);
    }
}
