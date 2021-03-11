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
    public class GetUserConsumer : IConsumer<GetUserRequest>
    {
        private IUserRepository _repository;

        public GetUserConsumer([FromServices] IUserRepository repository)
        {
            _repository = repository;
        }


        public async Task Consume(ConsumeContext<GetUserRequest> context)
        {
            await context.RespondAsync<GetUserResponse>(_repository.Get(context.Message.Id));
        }
    }
}
