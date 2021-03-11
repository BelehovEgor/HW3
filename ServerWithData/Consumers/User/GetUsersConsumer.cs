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
    public class GetUsersConsumer : IConsumer<GetUsersRequest>
    {
        private IUserRepository _repository;
        
        public GetUsersConsumer([FromServices] IUserRepository repository)
        {
            _repository = repository;
        }


        public async Task Consume(ConsumeContext<GetUsersRequest> context)
        {
            await context.RespondAsync<GetUsersResponse>(_repository.GetAll());
        }
    }
}
