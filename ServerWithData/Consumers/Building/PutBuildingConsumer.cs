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
    public class PutBuildingConsumer : IConsumer<PutBuildingRequest>
    {
        private IBuildingRepository _repository;

        public PutBuildingConsumer([FromServices] IBuildingRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<PutBuildingRequest> context)
        {
            await context.RespondAsync<PutBuildingResponse>(_repository.Update(context.Message.Building));
        }
    }
}
