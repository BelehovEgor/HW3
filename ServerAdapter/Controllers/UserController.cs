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
        private ILogger _logger;

        public UserController([FromServices] ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger<UserController>();
        }

        [HttpGet("GetUsers")]
        public List<User> GetUsers([FromServices] IGetUsersCommand getCommand)
        {
            _logger.LogInformation("get users");
            return getCommand.Execute();
        }

        [HttpGet("GetUser")]
        public User GetUser([FromServices] IGetUserCommand getCommand, [FromQuery] Guid id)
        {
            _logger.LogInformation("get user");
            return getCommand.Execute(id);
        }

        [HttpDelete("DeleteUser")]
        public bool DeleteUser([FromServices] IDeleteCommand deleteCommand, [FromQuery] Guid id)
        {
            _logger.LogInformation("delete user {id}");
            return deleteCommand.Execute(id);
        }

        [HttpPost("CreateUser")]
        public User CreateUser([FromServices] IPostCommand postCommand, [FromBody] User user)
        {
            _logger.LogInformation("create user");
            return postCommand.Execute(user);
        }

        [HttpPut("UpdateUser")]
        public void UpdateUser([FromServices] IPutCommand patchCommand, [FromBody] User user)
        {
            _logger.LogInformation("update user");
            patchCommand.Execute(user);
        }
    }
}
