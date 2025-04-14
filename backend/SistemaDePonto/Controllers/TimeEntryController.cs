using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaDePonto.Application.Interfaces;
using SistemaDePonto.Application.UseCases;

namespace SistemaDePonto.API.Controllers
{
    [Authorize]
    public class TimeEntryController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IListTimeEntriesByDateUseCase _listTimeEntriesByDateUseCase;
        private readonly IRegisterTimeEntryUseCase _registerUseCase;

        public TimeEntryController(
            IUserService userService,
            IListTimeEntriesByDateUseCase listTimeEntriesByDateUseCase,
            IRegisterTimeEntryUseCase registerUseCase)
        {
            _userService = userService;
            _listTimeEntriesByDateUseCase = listTimeEntriesByDateUseCase;
            _registerUseCase = registerUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<List<ListTimeEntriesByDateResponse>>> GetTimeEntriesByDate([FromQuery] ListTimeEntriesByDateRequest request)
        {
            var user = await _userService.GetOrCreateCurrentUserAsync();
            var result = await _listTimeEntriesByDateUseCase.ExecuteAsync(user.Id, request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RegisterTimeEntryResponse>> RegisterTime([FromBody] RegisterTimeEntryRequest request)
        {
            var user = await _userService.GetOrCreateCurrentUserAsync();
            var result = await _registerUseCase.ExecuteAsync(user.Id, request);
            return Ok(result);
        }
    }
}
