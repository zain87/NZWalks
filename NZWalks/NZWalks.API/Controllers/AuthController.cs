using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this._userRepository = userRepository;
            this._tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Models.DTO.LoginRequest request)
        {
            //Validate incoming request using fluent validation

            //Authenticate user i.e. check username and password
            var user = await _userRepository.Authenticate(request.UserName, request.Password);

            if (user != null)
            {
                //Generate Jwt Token
                var token = await _tokenHandler.Create(user);
                return Ok(token);
            }

            return BadRequest("Username or Password is incorrect.");
        }
    }
}
