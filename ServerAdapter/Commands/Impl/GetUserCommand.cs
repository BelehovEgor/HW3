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
    public class GetUserCommand : IGetUserCommand
    {
        private IRequestClient<GetUserRequest> _client;
        public GetUserCommand([FromServices] IRequestClient<GetUserRequest> client)
        {
            _client = client;
        }

        public User Execute(Guid id)
        {
            var answer = _client.GetResponse<GetUserResponse>(new GetUserRequest { Id = id });
            return answer.Result.Message.User;
        }
    }
}
