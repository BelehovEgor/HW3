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
    public class PutCommand : IPutCommand
    {
        private IRequestClient<PutUserRequest> _client;
        public PutCommand([FromServices] IRequestClient<PutUserRequest> client)
        {
            _client = client;
        }

        public void Execute(User user)
        {
            var answer = _client.GetResponse<PutUserResponse>(new PutUserRequest { User = user });
        }
    }
}
