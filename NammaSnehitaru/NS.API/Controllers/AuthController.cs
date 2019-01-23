using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NS.API.Data;
using NS.API.DTO;
using NS.API.Models;
using NS.API.Utility;

namespace NS.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo,IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register( //[FromBody]
                                                    UserRegisterDTO userRegisterDTO)
        {
            //Validating the Request
            // if(!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }

            userRegisterDTO.Username = userRegisterDTO.Username.ToLower();

            if(await _repo.IsUserExists(userRegisterDTO.Username))
                return BadRequest("Username already exists");

            var userToBeCreate = new User{
                Username = userRegisterDTO.Username
            };

            await _repo.Register(userToBeCreate,userRegisterDTO.Password);

            //201 OK Created
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO){

           // throw new Exception("We are the bad guys Exceptions");

            var _user = await _repo.Login(userLoginDTO.Username.ToLower(),userLoginDTO.Password);

            if(_user == null) return Unauthorized();

            var secertToken = _config.GetSection("AppSettings:Token").Value;

            TokenDTO token = PasswordUtility.CreateToken(_user,secertToken);

            return Ok(token);
        }
    }
}