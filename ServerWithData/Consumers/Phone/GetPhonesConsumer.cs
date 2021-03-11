using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Request;
using Models.Response;
using ServerWithData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.Consumers
{
    public class GetPhonesConsumer : IConsumer<GetPhonesRequest>
    {
        private IPhoneRepository _repository;
        
        public GetPhonesConsumer([FromServices] IPhoneRepository repository)
        {
            _repository = repository;
        }


        public async Task Consume(ConsumeContext<GetPhonesRequest> context)
        {
            await context.RespondAsync<GetPhonesResponse>(_repository.GetAll());
        }
    }
}
