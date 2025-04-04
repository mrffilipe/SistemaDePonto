using SistemaDePonto.Domain.Entities;

namespace SistemaDePonto.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetOrCreateCurrentUserAsync();
    }
}
