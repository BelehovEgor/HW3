using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerAdapter.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using MassTransit;
using Models.Request;
using Models.Response;

namespace ServerAdapter.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class PhoneController : ControllerBase
    {
        [HttpGet("GetPhones")]
        public List<Phone> GetPhones([FromServices] IRequestClient<GetPhonesRequest> client)
        {
            var answer = client.GetResponse<GetPhonesResponse>(new GetPhonesRequest());
            return answer.Result.Message.Phones;
        }

        [HttpGet("GetPhone")]
        public Phone GetPhone([FromServices] IRequestClient<GetPhoneRequest> client, [FromQuery] Guid id)
        {
            var answer = client.GetResponse<GetPhoneResponse>(new GetPhoneRequest { Id = id });
            return answer.Result.Message.Phone;
        }

        [HttpDelete("DeletePhone")]
        public bool DeletePhone([FromServices] IRequestClient<DeletePhoneRequest> client, [FromQuery] Guid id)
        {
            var answer = client.GetResponse<DeletePhoneResponse>(new DeletePhoneRequest { Id = id });
            return answer.Result.Message.IsSuccess;
        }

        [HttpPost("CreatePhone")]
        public Phone CreatePhone([FromServices] IRequestClient<PostPhoneRequest> client, [FromBody] Phone phone)
        {
            var answer = client.GetResponse<PostPhoneResponse>(new PostPhoneRequest { Phone = phone });
            return answer.Result.Message.Phone;
        }

        [HttpPut("UpdatePhone")]
        public void UpdateBuilding([FromServices] IRequestClient<PutPhoneRequest> client, [FromBody] Phone phone)
        {
            client.GetResponse<PutPhoneResponse>(new PutPhoneRequest { Phone = phone });
        }
    }
}
