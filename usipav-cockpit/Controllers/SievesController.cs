using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using usipav_cockpit.Application.Interfaces;
using usipav_cockpit.Domain.ViewModels;

namespace usipav_cockpit.Controllers
{
    [Route("Sieves")]
    public class SievesController : Controller
    {
        private readonly ILogger<SievesController> _logger;
        private readonly ISieveService _service;
        private readonly IAuthenticationService _authenticationService;

        public SievesController(ILogger<SievesController> logger, ISieveService service, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _service = service;
            _authenticationService = authenticationService;
        }

        [Authorize]
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult> Get()
        {
            var response = new Helpers.HttpResponse();

            response.Content = await _service.GetAsync();
            response.Message = "Peneiras retornadas com sucesso!";

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SieveViewModel viewModel)
        {
            viewModel.Active = true;

            await _service.PostAsync(viewModel);

            return NoContent();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SieveViewModel viewModel)
        {
            await _service.PutAsync(viewModel);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }

        public async Task<IActionResult> Index(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Users");
            }

            if (_authenticationService.ValidateToken(token))
            {
                var model = await _service.GetAsync(x => x.Active);
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
    }
}
