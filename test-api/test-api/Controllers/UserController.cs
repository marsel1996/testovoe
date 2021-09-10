using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using test_api.Application.UserApplication.Commands.EditPasswordCommand;
using test_api.Application.UserApplication.Commands.EditUserCommand;
using test_api.Application.UserApplication.Commands.RemoveUser;
using test_api.Application.UserApplication.Commands.SaveUserCommand;
using test_api.Application.UserApplication.Queries.GetRolesListQuery;
using test_api.Application.UserApplication.Queries.GetUserByIdQuery;
using test_api.Application.UserApplication.Queries.GetUserInfoQuery;
using test_api.Application.UserApplication.Queries.GetUserListQuery;
using test_api.Helpers;

namespace test_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<GetUserListItemDto[]> GetUserList([FromQuery] GetUserListQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("user")]
        public async Task<GetUserByIdDto> GetUserById([FromQuery] GetUserByIdQuery query)
        {
            return await _mediator.Send(query);
        }
        

        [HttpGet("info")]
        public async Task<GetUserInfoDto> GetUserInfo([FromQuery] GetUserInfoQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveUser([FromBody] SaveUserCommand command)
        {
            return await _mediator.Send(command).ToActionResult();
        }
        
        [HttpPut("edit")]
        public async Task<IActionResult> EditUser([FromBody] EditUserCommand command)
        {
            return await _mediator.Send(command).ToActionResult();
        }

        [HttpPatch("edit-password")]
        public async Task<IActionResult> EditPassword([FromBody] EditPasswordCommand command)
        {
            return await _mediator.Send(command).ToActionResult();
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveUser([FromQuery] RemoveUserCommand command)
        {
            return await _mediator.Send(command).ToActionResult();
        }

        [HttpGet("roles")]
        public async Task<GetUserRolesListItemDto[]> GetUserRoleList([FromQuery] GetUserRoleListQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
