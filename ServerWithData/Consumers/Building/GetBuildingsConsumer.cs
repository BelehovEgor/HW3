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
    public class GetBuildingsConsumer : IConsumer<GetBuildingsRequest>
    {
        private IBuildingRepository _repository;

        public GetBuildingsConsumer([FromServices] IBuildingRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<GetBuildingsRequest> context)
        {
            await context.RespondAsync<GetBuildingsResponse>(_repository.GetAll());
        }
    }
}
