using SistemaDePonto.Application.Interfaces;
using SistemaDePonto.Domain.Entities;
using SistemaDePonto.Domain.Interfaces;

namespace SistemaDePonto.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;

        public UserService(IUserRepository userRepository, ICurrentUser currentUser)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
        }

        public async Task<User> GetOrCreateCurrentUserAsync()
        {
            var uid = await _currentUser.GetFirebaseUidAsync();

            var user = await _userRepository.GetByFirebaseUidAsync(uid);
            if (user is not null)
            {
                return user;
            }

            var email = await _currentUser.GetEmailAsync();
            var fullName = await _currentUser.GetFullNameAsync();

            var newUser = new User(uid, fullName, email);
            newUser = await _userRepository.AddAsync(newUser);

            return newUser;
        }
    }
}
