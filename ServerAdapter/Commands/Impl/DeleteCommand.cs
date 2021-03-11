using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAdapter.Commands.Impl
{
    public class DeleteCommand : IDeleteCommand
    {
        private IRequestClient<DeleteUserRequest> _client;
        public DeleteCommand([FromServices] IRequestClient<DeleteUserRequest> client)
        {
            _client = client;
        }

        public bool Execute(Guid id)
        {
            var answer = _client.GetResponse<DeleteUserResponse>(new DeleteUserRequest { Id = id });
            return answer.Result.Message.IsSuccess;
        }
    }
}
