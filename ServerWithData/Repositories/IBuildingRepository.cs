using Models;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.Repositories
{
    public interface IBuildingRepository
    {
        GetBuildingResponse Get(Guid id);
        DeleteBuildingResponse Remove(Guid id);
        PostBuildingResponse Add(Building build);
        PutBuildingResponse Update(Building build);
        GetBuildingsResponse GetAll();
    }
}
