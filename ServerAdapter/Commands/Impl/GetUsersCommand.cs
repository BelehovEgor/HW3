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
    public class GetUsersCommand : IGetUsersCommand
    {
        private IRequestClient<GetUsersRequest> _client;
        public GetUsersCommand([FromServices] IRequestClient<GetUsersRequest> client)
        {
            _client = client;
        }

        public List<User> Execute()
        {
            var answer = _client.GetResponse<GetUsersResponse>(new GetUsersRequest());
            return answer.Result.Message.Users;
        }
    }
}
