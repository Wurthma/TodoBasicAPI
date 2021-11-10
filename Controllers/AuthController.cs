using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoBasicAPI.Models;
using TodoBasicAPI.Repositories;

namespace TodoBasicAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User userModel)
        {
            var userRepository = new UserRepository();

            var user = userRepository.Get(userModel.Username, userModel.Password);

            if (user == null)
                return NotFound(new { Message = "Usuário ou senha inválidos" });

            var token = Services.TokenService.GenerateToken(user);
            user.Password = "";

            return new {
                user = user,
                token = token
            };
        }
    }
}