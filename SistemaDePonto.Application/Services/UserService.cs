using SistemaDePonto.Application.Interfaces;
using SistemaDePonto.Domain.Entities;

namespace SistemaDePonto.Application.Services
{
    public class UserService : IUserService
    {
        public Task<User?> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetOrCreateCurrentUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
