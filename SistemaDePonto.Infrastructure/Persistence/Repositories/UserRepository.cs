using Microsoft.EntityFrameworkCore;
using SistemaDePonto.Domain.Entities;
using SistemaDePonto.Domain.Interfaces;

namespace SistemaDePonto.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByFirebaseUidAsync(string firebaseUid)
        {
            return await _context.Users
              .FirstOrDefaultAsync(u => u.FirebaseUid == firebaseUid);
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
