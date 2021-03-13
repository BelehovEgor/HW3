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
    public class PostUserConsumer : IConsumer<PostUserRequest>
    {
        private IUserRepository _repository;

        public PostUserConsumer([FromServices] IUserRepository repository)
        {
            _repository = repository;
        }


        public async Task Consume(ConsumeContext<PostUserRequest> context)
        {
            await context.RespondAsync<PostUserResponse>(_repository.Add(context.Message.User));
        }
    }
}
