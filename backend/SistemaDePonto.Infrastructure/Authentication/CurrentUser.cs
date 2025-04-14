using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SistemaDePonto.Application.Interfaces;
using SistemaDePonto.Domain.Interfaces;

namespace SistemaDePonto.Infrastructure.Authentication
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public CurrentUser(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        private ClaimsPrincipal User =>
            _httpContextAccessor.HttpContext?.User
            ?? throw new UnauthorizedAccessException("Usuário não autenticado.");

        public async Task<Guid> GetUserIdAsync()
        {
            var firebaseUid = await GetFirebaseUidAsync();

            var user = await _userRepository.GetByFirebaseUidAsync(firebaseUid);
            if (user is null)
            {
                throw new UnauthorizedAccessException("Usuário não registrado.");
            }

            return user.Id;
        }

        public Task<string> GetFirebaseUidAsync()
        {
            var uid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(uid))
            {
                throw new UnauthorizedAccessException("UID não encontrado.");
            }

            return Task.FromResult(uid);
        }

        public Task<string> GetFullNameAsync()
        {
            var fullName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;

            return Task.FromResult(fullName);
        }

        public Task<string> GetEmailAsync()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;

            return Task.FromResult(email);
        }
    }
}
