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
    public class GetBuildingConsumer : IConsumer<GetBuildingRequest>
    {
        private IBuildingRepository _repository;

        public GetBuildingConsumer([FromServices] IBuildingRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<GetBuildingRequest> context)
        {
            await context.RespondAsync<GetBuildingResponse>(_repository.Get(context.Message.Id));
        }
    }
}
