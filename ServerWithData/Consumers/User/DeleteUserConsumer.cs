using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Models.Response;
using ServerWithData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.Consumers
{
    public class DeleteUserConsumer : IConsumer<DeleteUserRequest>
    {
        private IUserRepository _repository;

        public DeleteUserConsumer([FromServices] IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<DeleteUserRequest> context)
        {
            await context.RespondAsync<DeleteUserResponse>(_repository.Remove(context.Message.Id));
        }
    }
}
