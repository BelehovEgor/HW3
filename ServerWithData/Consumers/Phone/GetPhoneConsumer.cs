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
    public class GetPhoneConsumer : IConsumer<GetPhoneRequest>
    {
        private IPhoneRepository _repository;

        public GetPhoneConsumer([FromServices] IPhoneRepository repository)
        {
            _repository = repository;
        }


        public async Task Consume(ConsumeContext<GetPhoneRequest> context)
        {
            await context.RespondAsync<GetPhoneResponse>(_repository.Get(context.Message.Id));
        }
    }
}
