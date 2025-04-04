using SistemaDePonto.Domain.Entities;
using SistemaDePonto.Domain.Interfaces;

namespace SistemaDePonto.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User?> GetByFirebaseUidAsync(string firebaseUid)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
