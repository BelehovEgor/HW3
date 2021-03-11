using Models;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.Repositories
{
    public interface IPhoneRepository
    {
        GetPhoneResponse Get(Guid id);
        DeletePhoneResponse Remove(Guid id);
        PostPhoneResponse Add(Phone phone);
        PutPhoneResponse Update(Phone phone);
        GetPhonesResponse GetAll();
    }
}
