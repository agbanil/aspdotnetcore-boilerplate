using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvatarController : ControllerBase
    {
        private readonly IAvatarService _avatarService;
        private readonly ILogger<AvatarController> _logger;

        public AvatarController(IAvatarService avatarService, ILogger<AvatarController> logger)
        {
            _avatarService = avatarService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetAvatar(string name)
        {
            if (name == null || name.Equals("")) {
                _logger.LogError("'name' param not passed in request");
                return BadRequest("Please send a 'name' param for which an avatar should be returned");
            }
            var accounts = await _avatarService.GetByName(name);
            return Ok(accounts);
        }

    }
}
