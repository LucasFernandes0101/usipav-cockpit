using usipav_cockpit.Domain.Entities;
using usipav_cockpit.Domain.Interfaces;
using usipav_cockpit.Infrastructure.Contexts;

namespace usipav_cockpit.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(SqlDbContext dbContext) : base(dbContext)
        {
        }
    }
}
