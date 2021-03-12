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
    public class BuildingRepository : IBuildingRepository
    {
        private AppContext _context;
        private IMapper<Building, DbBuilding> _mapper;

        public BuildingRepository([FromServices] AppContext context, [FromServices] IMapper<Building, DbBuilding> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public PostBuildingResponse Add(Building building)
        {
            var res = _context.Buildings.Where(b => b.Id == building.Id).FirstOrDefault();
            if (res == null)
            {
                var dbbuilding = _mapper.GetBack(building);
                var owner = _context.Users.Where(u => dbbuilding.OwnerId == u.Id).FirstOrDefault();
                if (owner != null)
                {
                    dbbuilding.Owner = owner;
                    owner.Buildings.Add(dbbuilding);
                }
                else
                {
                    dbbuilding.OwnerId = null;
                }
                var phone = _context.Phones.Where(p => p.Id == dbbuilding.PhoneId).FirstOrDefault();
                if (phone != null)
                {
                    dbbuilding.Phone = phone;
                    phone.Building = dbbuilding;
                }
                else
                {
                    dbbuilding.PhoneId = null;
                }
                _context.Buildings.Add(dbbuilding);
                _context.SaveChanges();
                return new PostBuildingResponse { Building = _mapper.GetFront(dbbuilding) };
            }
            return new PostBuildingResponse{ Building = _mapper.GetFront(res) };
        }

        public GetBuildingResponse Get(Guid id)
        {
            return new GetBuildingResponse { Building = _mapper.GetFront(_context.Buildings.Where(b => b.Id == id).FirstOrDefault())};
        }

        public GetBuildingsResponse GetAll()
        {
            var answer = new GetBuildingsResponse { Buildings = new List<Building>() };
            foreach(var dbbuilding in _context.Buildings)
            {
                answer.Buildings.Add(_mapper.GetFront(dbbuilding));
            }
            return answer;
        }

        public DeleteBuildingResponse Remove(Guid id)
        {
            var res = _context.Buildings.Where(b => b.Id == id).FirstOrDefault();
            if(res == null)
                return new DeleteBuildingResponse { IsSuccess = false };

            _context.Buildings.Remove(res);
            _context.SaveChanges();
            return new DeleteBuildingResponse { IsSuccess = true };
        }

        public PutBuildingResponse Update(Building building)
        {
            var res = _context.Buildings.Where(b => b.Id == building.Id).FirstOrDefault();
            if (res != null)
            {
                var dbbuilding = _mapper.GetBack(building);
                var owner = _context.Users.Where(u => dbbuilding.OwnerId == u.Id).FirstOrDefault();
                if (owner != null)
                {
                    dbbuilding.Owner = owner;
                    owner.Buildings.Add(dbbuilding);
                    //_context.Links.Add(new DbLinkBuildingUser { Id = Guid.NewGuid(), BuildingId = dbbuilding.Id, UserId = owner.Id });
                }
                else
                {
                    dbbuilding.OwnerId = null;
                }
                var phone = _context.Phones.Where(p => p.Id == dbbuilding.PhoneId).FirstOrDefault();
                if (phone != null)
                {
                    dbbuilding.Phone = phone;
                    phone.Building = dbbuilding;
                }
                else
                {
                    dbbuilding.PhoneId = null;
                }

                _context.Buildings.Update(dbbuilding);

                _context.SaveChanges();
                return new PutBuildingResponse();
            }
            return new PutBuildingResponse();
        }
    }
}
