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
    public class PostBuildingConsumer : IConsumer<PostBuildingRequest>
    {
        private IBuildingRepository _repository;

        public PostBuildingConsumer([FromServices] IBuildingRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<PostBuildingRequest> context)
        {
            await context.RespondAsync<PostBuildingResponse>(_repository.Add(context.Message.Building));
        }
    }
}
