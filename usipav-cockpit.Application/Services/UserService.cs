using AutoMapper;
using System.Linq.Expressions;
using usipav_cockpit.Application.Interfaces;
using usipav_cockpit.Domain.Entities;
using usipav_cockpit.Domain.Interfaces;
using usipav_cockpit.Domain.ViewModels;
using usipav_cockpit.Infrastructure.Repositories;

namespace usipav_cockpit.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IAuthenticationService _authService;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository,
            IAuthenticationService authService,
            IMapper mapper)
        {
            _repository = repository;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserViewModel>> GetAsync(Expression<Func<User, bool>>? criteria = null)
        {
            var result = await _repository.GetAsync(criteria);
            return _mapper.Map<IEnumerable<UserViewModel>>(result);
        }

        private async Task<bool> ValidateIfUserAlreadyExistsAsync(string email)
        {
            var users = await _repository.GetAsync(x => x.Email == email);

            if (users.Any())
                return true;

            return false;
        }

        public async Task<string> PostAsync(PostUserViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.Email))
                throw new Exception("O e-mail é obrigatório!");

            var alreadyExists = await ValidateIfUserAlreadyExistsAsync(viewModel.Email);

            if(alreadyExists)
                throw new Exception("O e-mail informado já está sendo utilizado!");

            var entity = _mapper.Map<User>(viewModel);

            if (string.IsNullOrEmpty(viewModel.Password) || string.IsNullOrEmpty(viewModel.ConfirmationPassword))
                throw new Exception("A senha não pode ser vazia!");

            if (!viewModel.Password.SequenceEqual(viewModel.ConfirmationPassword))
                throw new Exception("As senhas não coincidem!");

            entity.Password = _authService.EncryptUserPassword(viewModel.Password);

            await _repository.AddAsync(entity);

            return await _authService.GenerateUserJWTAsync(entity);
        }

        public async Task PutAsync(UserViewModel user)
        {
            var model = _mapper.Map<User>(user);
            await _repository.UpdateAsync(model);
        }

        public async Task<string> ValidateUserPasswordAsync(LoginViewModel login)
        {
            if (string.IsNullOrWhiteSpace(login.Email))
                throw new Exception("O e-mail é obrigatório para login!");

            if (string.IsNullOrWhiteSpace(login.Password))
                throw new Exception("A senha é obrigatória para login!");

            var user = (await _repository.GetAsync(x => x.Email == login.Email)).FirstOrDefault();

            if (user is null)
                throw new Exception("Usuário não existe!");

            string token = await _authService.ValidateUserPasswordAsync(user, login.Password);

            if (string.IsNullOrWhiteSpace(token))
                throw new Exception("Senha incorreta!");

            return token;
        }
    }
}