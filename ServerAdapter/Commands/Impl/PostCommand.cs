using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAdapter.Commands.Impl
{
    public class PostCommand : IPostCommand
    {
        private IRequestClient<PostUserRequest> _client;
        public PostCommand([FromServices] IRequestClient<PostUserRequest> client)
        {
            _client = client;
        }

        public User Execute(User user)
        {
            var answer = _client.GetResponse<PostUserResponse>(new PostUserRequest { User = user });
            return answer.Result.Message.User;
        }
    }
}
