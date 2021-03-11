using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAdapter.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class BuildingController : Controller
    {
        [HttpGet("GetBuildings")]
        public List<Building> GetBuildings([FromServices] IRequestClient<GetBuildingsRequest> client)
        {
            var answer = client.GetResponse<GetBuildingsResponse>(new GetBuildingsRequest());
            return answer.Result.Message.Buildings;
        }

        [HttpGet("GetBuilding")]
        public Building GetBuilding([FromServices] IRequestClient<GetBuildingRequest> client, [FromQuery] Guid id)
        {
            var answer = client.GetResponse<GetBuildingResponse>(new GetBuildingRequest { Id = id });
            return answer.Result.Message.Building;
        }

        [HttpDelete("DeleteBuilding")]
        public bool DeleteBuilding([FromServices] IRequestClient<DeleteBuildingRequest> client, [FromQuery] Guid id)
        {
            var answer = client.GetResponse<DeleteBuildingResponse>(new DeleteBuildingRequest { Id = id });
            return answer.Result.Message.IsSuccess;
        }

        [HttpPost("CreateBuilding")]
        public Building CreateBuilding([FromServices] IRequestClient<PostBuildingRequest> client, [FromBody] Building building)
        {
            var answer = client.GetResponse<PostBuildingResponse>(new PostBuildingRequest { Building = building });
            return answer.Result.Message.Building;
        }

        [HttpPut("UpdateBuilding")]
        public void UpdateBuilding([FromServices] IRequestClient<PutBuildingRequest> client, [FromBody] Building building)
        {
            client.GetResponse<PutBuildingResponse>(new PutBuildingRequest { Building = building });
        }
    }
}
