using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Response;
using ServerWithData.DbEntity;
using ServerWithData.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.Repositories.Impl
{
    class UserRepository : IUserRepository
    {
        private AppContext _context;
        private IMapper<User, DbUser> _mapper;

        public UserRepository([FromServices] AppContext context, [FromServices] IMapper<User, DbUser> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public PostUserResponse Add(User user)
        {
            var res = _context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            if (res == null)
            {
                var dbUser = _mapper.GetBack(user);
                _context.Users.Add(dbUser);
                _context.SaveChanges();
                return new PostUserResponse { User = user };
            }
            return new PostUserResponse { User = _mapper.GetFront(res) };
        }

        public GetUserResponse Get(Guid id)
        {
            return new GetUserResponse { User = _mapper.GetFront(_context.Users.Where(u => u.Id == id).FirstOrDefault()) };
        }

        public GetUsersResponse GetAll()
        {
            var answer = new GetUsersResponse { Users = new List<User>() };
            foreach (var dbuser in _context.Users)
            {
                answer.Users.Add(_mapper.GetFront(dbuser));
            }
            return answer;
        }

        public DeleteUserResponse Remove(Guid id)
        {
            var dbuser = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            if (dbuser == null)
                return new DeleteUserResponse { IsSuccess = false };

            _context.Users.Remove(dbuser);
            _context.SaveChanges();
            return new DeleteUserResponse { IsSuccess = true };
        }

        public PutUserResponse Update(User user)
        {
            _context.Users.Update(_mapper.GetBack(user));
            _context.SaveChanges();

            return new PutUserResponse();
        }
    }
}
