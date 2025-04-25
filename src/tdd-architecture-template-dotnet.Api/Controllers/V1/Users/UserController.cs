using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tdd_architecture_template_dotnet.Application.Services.Users.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Users;
using tdd_architecture_template_dotnet.Domain.Enums;

namespace tdd_architecture_template_dotnet.Controllers.V1.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.GetAll();

            if (response.StatusCode == (int)HttpStatus.BadRequest)
                return BadRequest(response);

            return Ok(response);

        }

        [HttpGet("GetUserById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _userService.GetById(id);

            if (response.StatusCode == (int)HttpStatus.BadRequest)
                return BadRequest(response);

            return Ok(response);

        }

        [HttpPut("PutUser")]
        [Authorize]
        public async Task<ActionResult<UserViewModel>> Put([FromBody] UserViewModel user)
        {
            var response = await _userService.Put(user);

            if (response.StatusCode == (int)HttpStatus.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("PostUser")]
        [Authorize]
        public async Task<ActionResult<UserViewModel>> Post([FromBody] UserViewModel user)
        {
            var response = await _userService.Post(user);

            if (response.StatusCode == (int)HttpStatus.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("DeleteUser/{userId:int}")]
        public async Task<ActionResult<UserViewModel>> Delete([FromRoute] int userId)
        {
            var response = await _userService.Delete(userId);

            if (response.StatusCode == (int)HttpStatus.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
