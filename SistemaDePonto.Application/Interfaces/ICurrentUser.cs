namespace SistemaDePonto.Application.Interfaces
{
    public interface ICurrentUser
    {
        Task<Guid> GetUserIdAsync();
        Task<string> GetFirebaseUidAsync();
        Task<string> FullNameAsync();
        Task<string> GetEmailAsync();
    }
}
