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
    public class PutUserConsumer : IConsumer<PutUserRequest>
    {
        private IUserRepository _repository;

        public PutUserConsumer([FromServices] IUserRepository repository)
        {
            _repository = repository;
        }


        public async Task Consume(ConsumeContext<PutUserRequest> context)
        {
            await context.RespondAsync<PutUserResponse>(_repository.Update(context.Message.User));
        }
    }
}
