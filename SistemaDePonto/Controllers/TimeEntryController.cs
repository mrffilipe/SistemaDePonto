using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaDePonto.Application.Interfaces;
using SistemaDePonto.Application.UseCases;

namespace SistemaDePonto.API.Controllers
{
    [Authorize]
    public class TimeEntryController : BaseController
    {
        private readonly ICurrentUser _currentUser;
        private readonly IListTimeEntriesByDateUseCase _listTimeEntriesByDateUseCase;
        private readonly IRegisterTimeEntryUseCase _registerUseCase;

        public TimeEntryController(
            ICurrentUser currentUser,
            IListTimeEntriesByDateUseCase listTimeEntriesByDateUseCase,
            IRegisterTimeEntryUseCase registerUseCase)
        {
            _currentUser = currentUser;
            _listTimeEntriesByDateUseCase = listTimeEntriesByDateUseCase;
            _registerUseCase = registerUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<List<ListTimeEntriesByDateResponse>>> GetTimeEntriesByDate([FromQuery] ListTimeEntriesByDateRequest request)
        {
            var userId = await _currentUser.GetUserIdAsync();
            var result = await _listTimeEntriesByDateUseCase.ExecuteAsync(userId, request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RegisterTimeEntryResponse>> RegisterTime([FromBody] RegisterTimeEntryRequest request)
        {
            var userId = await _currentUser.GetUserIdAsync();
            var result = await _registerUseCase.ExecuteAsync(userId, request);
            return Ok(result);
        }
    }
}
