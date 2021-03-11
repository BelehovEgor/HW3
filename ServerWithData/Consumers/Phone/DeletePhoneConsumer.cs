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
    public class DeletePhoneConsumer : IConsumer<DeletePhoneRequest>
    {
        private IPhoneRepository _repository;

        public DeletePhoneConsumer([FromServices] IPhoneRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<DeletePhoneRequest> context)
        {
            await context.RespondAsync<DeletePhoneResponse>(_repository.Remove(context.Message.Id));
        }
    }
}
