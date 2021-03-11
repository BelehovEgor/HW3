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
    public class DeleteBuildingConsumer : IConsumer<DeleteBuildingRequest>
    {
        private IBuildingRepository _repository;

        public DeleteBuildingConsumer([FromServices] IBuildingRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<DeleteBuildingRequest> context)
        {
            await context.RespondAsync<DeleteBuildingResponse>(_repository.Remove(context.Message.Id));
        }
    }
}
