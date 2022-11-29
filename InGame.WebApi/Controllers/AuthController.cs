using System.Linq;
using System.Threading.Tasks;
using InGame.Business.Concrete.DTO.Concrete.User;
using InGame.Business.Concrete.Enum;
using InGame.Business.Interface;
using InGame.Business.Tools.Validations;
using InGame.WebApi.CustomFilters;
using Microsoft.AspNetCore.Mvc;

namespace InGame.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Register")]
        [ValidModel]
        public async Task<IActionResult> RegisterAsync(UserRegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(registerDto);
                if (result.ServiceResultType==ServiceResultType.Success) return Ok();
                return BadRequest(result.Data);
            }

            return BadRequest("Some proporties are not valid");
        }
    }
}
