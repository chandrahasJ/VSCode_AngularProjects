using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS.API.Data;
using NS.API.DTO;

namespace NS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IDatingRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var User = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailedDTO>(User);

            return Ok(userToReturn);
        }

         [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
              var Users = await _repo.GetUsers();

              var userToReturn = _mapper.Map<IEnumerable<UserForListDTO>>(Users);

              return Ok(userToReturn);
        }
    }
}