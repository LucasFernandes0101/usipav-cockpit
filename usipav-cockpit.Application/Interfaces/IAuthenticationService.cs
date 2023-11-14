using usipav_cockpit.Domain.Entities;

namespace usipav_cockpit.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> ValidateUserPasswordAsync(User user, string password);
        Task<string> GenerateUserJWTAsync(User user);
        public bool ValidateToken(string token);
        string EncryptUserPassword(string password);
    }
}
