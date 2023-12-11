using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parkside.Services.UsersExtend;

namespace Parkside.Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserExtendController : ControllerBase
    {
        private readonly IUserExtendService _userExtendService;
        private readonly ILogger<UserExtendController> _logger;

        public UserExtendController(IUserExtendService userExtendService, ILogger<UserExtendController> logger)
        {
            _userExtendService = userExtendService;
            _logger = logger;
        }

        [HttpGet("getUsers")]
        [ResponseCache(CacheProfileName = "Cache2Mins")]
        public IActionResult GetUsers()
        {
            var users = _userExtendService.GetUsers();
            return Ok(users);
        }
    }
}
