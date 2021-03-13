using Models;
using ServerWithData.DbEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.Mapper.Impl
{
    public class BuildingMapper : IMapper<Building, DbBuilding>
    {
        public DbBuilding GetBack(Building front)
        {
            if (front == null)
                return null;
            return new DbBuilding { Id = front.Id, Address = front.Address, OwnerId = front.OwnerId, PhoneId = front.PhoneId };
        }

        public Building GetFront(DbBuilding back)
        {
            if (back == null)
                return null;
            return new Building { Id = back.Id, Address = back.Address, OwnerId = back.OwnerId, PhoneId = back.PhoneId };
        }
    }
}
