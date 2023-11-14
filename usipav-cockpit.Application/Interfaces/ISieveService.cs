using System.Linq.Expressions;
using usipav_cockpit.Domain.Entities;
using usipav_cockpit.Domain.ViewModels;

namespace usipav_cockpit.Application.Interfaces
{
    public interface ISieveService
    {
        Task<IEnumerable<SieveViewModel>> GetAsync(Expression<Func<Sieve, bool>>? criteria = null);
        Task PostAsync(SieveViewModel sieve);
        Task PutAsync(SieveViewModel sieve);
        Task DeleteAsync(int id);
    }
}
