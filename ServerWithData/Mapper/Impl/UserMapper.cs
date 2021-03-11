using Models;
using ServerWithData.DbEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.Mapper.Impl
{
    public class UserMapper : IMapper<User, DbUser>
    {
        public DbUser GetBack(User front)
        {
            if (front == null)
                return null;
            return new DbUser { Id = front.Id, Age = front.Age, Lastname = front.Lastname, Name = front.Name };
        }

        public User GetFront(DbUser back)
        {
            if (back == null)
                return null;
            return new User { Id = back.Id, Age = back.Age ?? 0, Name = back.Name, Lastname = back.Lastname };
        }
    }
}
