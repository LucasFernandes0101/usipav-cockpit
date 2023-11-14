using System.Linq.Expressions;
using usipav_cockpit.Domain.Entities;
using usipav_cockpit.Domain.ViewModels;

namespace usipav_cockpit.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAsync(Expression<Func<User, bool>>? criteria = null);
        Task<string> PostAsync(PostUserViewModel user);
        Task PutAsync(UserViewModel user);
        Task<string> ValidateUserPasswordAsync(LoginViewModel login);
    }
}
