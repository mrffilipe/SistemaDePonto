using SistemaDePonto.Domain.Entities;

namespace SistemaDePonto.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByFirebaseUidAsync(string firebaseUid);
        Task<User> AddAsync(User user);
    }
}
