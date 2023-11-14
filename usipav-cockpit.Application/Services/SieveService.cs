using AutoMapper;
using System.Linq.Expressions;
using usipav_cockpit.Application.Interfaces;
using usipav_cockpit.Domain.Entities;
using usipav_cockpit.Domain.Interfaces;
using usipav_cockpit.Domain.ViewModels;

namespace usipav_cockpit.Application.Services
{
    public class SieveService : ISieveService
    {
        private readonly ISieveRepository _repository;
        private readonly IMapper _mapper;
        public SieveService(ISieveRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SieveViewModel>> GetAsync(Expression<Func<Sieve, bool>>? criteria = null)
        {
            var result = await _repository.GetAsync(criteria);
            return _mapper.Map<IEnumerable<SieveViewModel>>(result);
        }

        public async Task PostAsync(SieveViewModel sieve)
        {
            var model = _mapper.Map<Sieve>(sieve);
            await _repository.AddAsync(model);
        }

        public async Task PutAsync(SieveViewModel sieve)
        {
            var model = _mapper.Map<Sieve>(sieve);
            await _repository.UpdateAsync(model);
        }
    }
}
