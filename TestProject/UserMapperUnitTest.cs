using Models;
using NUnit.Framework;
using ServerWithData.DbEntity;
using ServerWithData.Mapper;
using ServerWithData.Mapper.Impl;
using System;

namespace TestProject
{
    public class UserMapperUnitTest
    {
        private IMapper<User, DbUser> _mapper;
        private DbUser _dbUser;
        private User _user;

        [OneTimeSetUp]
        public void Setup()
        {
            _mapper = new UserMapper();
            _dbUser = new DbUser { Id = Guid.NewGuid(), Age = 10, Name = "name", Lastname = "lastname" };
            _user = new User { Id = _dbUser.Id, Age = 10, Name = "name", Lastname = "lastname" };
        }

        [Test]
        public void ConvertBuildingToDbBuilding()
        {
            var user = _mapper.GetFront(_dbUser);
            Assert.AreEqual(user.Id, _dbUser.Id);
            Assert.AreEqual(user.Name, _dbUser.Name);
            Assert.AreEqual(user.Lastname, _dbUser.Lastname);
            Assert.AreEqual(user.Age, _dbUser.Age);
        }

        [Test]
        public void ConvertDbBuildingToBuilding()
        {
            var user = _mapper.GetBack(_user);
            Assert.AreEqual(user.Id, _user.Id);
            Assert.AreEqual(user.Name, _user.Name);
            Assert.AreEqual(user.Lastname, _user.Lastname);
            Assert.AreEqual(user.Age, _user.Age);
        }

        [Test]
        public void ConvertNull()
        {
            Assert.IsNull(_mapper.GetBack(null));
            Assert.IsNull(_mapper.GetFront(null));
        }
    }
}