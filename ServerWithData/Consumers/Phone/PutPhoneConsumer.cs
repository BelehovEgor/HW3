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
    public class PutPhoneConsumer : IConsumer<PutPhoneRequest>
    {
        private IPhoneRepository _repository;

        public PutPhoneConsumer([FromServices] IPhoneRepository repository)
        {
            _repository = repository;
        }


        public async Task Consume(ConsumeContext<PutPhoneRequest> context)
        {
            await context.RespondAsync<PutPhoneResponse>(_repository.Update(context.Message.Phone));
        }
    }
}
