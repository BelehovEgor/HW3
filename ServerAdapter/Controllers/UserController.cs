using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerAdapter.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using MassTransit;

namespace ServerAdapter.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("GetUsers")]
        public List<User> GetUsers([FromServices] IGetUsersCommand getCommand)
        {
            return getCommand.Execute();
        }

        [HttpGet("GetUser")]
        public User GetUser([FromServices] IGetUserCommand getCommand, [FromQuery] Guid id)
        {
            return getCommand.Execute(id);
        }

        [HttpDelete("DeleteUser")]
        public bool DeleteUser([FromServices] IDeleteCommand deleteCommand, [FromQuery] Guid id)
        {
            return deleteCommand.Execute(id);
        }

        [HttpPost("CreateUser")]
        public User CreateUser([FromServices] IPostCommand postCommand, [FromBody] User user)
        {
            return postCommand.Execute(user);
        }

        [HttpPut("UpdateUser")]
        public void UpdateUser([FromServices] IPutCommand patchCommand, [FromBody] User user)
        {
            patchCommand.Execute(user);
        }
    }
}
