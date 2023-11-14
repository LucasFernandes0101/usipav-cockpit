using Microsoft.AspNetCore.Mvc;
using usipav_cockpit.Application.Interfaces;
using usipav_cockpit.Domain.ViewModels;

namespace usipav_cockpit.Controllers
{
    [Route("Users")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _service;

        public UsersController(ILogger<UsersController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostUserViewModel viewModel)
        {
            try
            {
                await _service.PostAsync(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            try
            {
                var token = await _service.ValidateUserPasswordAsync(login);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Login")]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

    }
}