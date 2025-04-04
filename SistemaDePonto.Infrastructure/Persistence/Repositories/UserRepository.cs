using SistemaDePonto.Domain.Entities;
using SistemaDePonto.Domain.Interfaces;

namespace SistemaDePonto.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<User?> GetByFirebaseUidAsync(string firebaseUid)
        {
            throw new NotImplementedException();
        }

        public async Task<User> AddAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
