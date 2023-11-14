using usipav_cockpit.Domain.Entities;
using usipav_cockpit.Domain.Interfaces;
using usipav_cockpit.Infrastructure.Contexts;

namespace usipav_cockpit.Infrastructure.Repositories
{
    public class SieveRepository : BaseRepository<Sieve>, ISieveRepository
    {
        public SieveRepository(SqlDbContext dbContext) : base(dbContext)
        {
        }
    }
}
