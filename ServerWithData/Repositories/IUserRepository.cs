using Models;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.Repositories
{
    public interface IUserRepository
    {
        GetUserResponse Get(Guid id);
        DeleteUserResponse Remove(Guid id);
        PostUserResponse Add(User user);
        PutUserResponse Update(User user);
        GetUsersResponse GetAll();
    }
}
