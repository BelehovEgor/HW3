using Models;
using ServerWithData.DbEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.Mapper.Impl
{
    public class PhoneMapper : IMapper<Phone, DbPhone>
    {
        public DbPhone GetBack(Phone front)
        {
            if (front == null)
                return null;
            return new DbPhone { Id = front.Id, Number = front.Number };
        }

        public Phone GetFront(DbPhone back)
        {
            if (back == null)
                return null;
            return new Phone { Id = back.Id, Number = back.Number };
        }
    }
}
