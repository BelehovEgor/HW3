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
    public class PostPhoneConsumer : IConsumer<PostPhoneRequest>
    {
        private IPhoneRepository _repository;

        public PostPhoneConsumer([FromServices] IPhoneRepository repository)
        {
            _repository = repository;
        }


        public async Task Consume(ConsumeContext<PostPhoneRequest> context)
        {
            await context.RespondAsync<PostPhoneResponse>(_repository.Add(context.Message.Phone));
        }
    }
}
