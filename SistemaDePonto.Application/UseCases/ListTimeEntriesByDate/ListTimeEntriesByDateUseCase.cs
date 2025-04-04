using SistemaDePonto.Application.Interfaces;

namespace SistemaDePonto.Application.UseCases
{
    public class ListTimeEntriesByDateUseCase : IListTimeEntriesByDateUseCase
    {
        public Task<List<ListTimeEntriesByDateResponse>> ExecuteAsync(Guid userId, ListTimeEntriesByDateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
