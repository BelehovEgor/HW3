using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var res = _context.Buildings.FirstOrDefault(b => b.Id == building.Id);
            if (res != null)
                return new PostBuildingResponse { Building = _mapper.GetFront(res) };

            var dbbuilding = _mapper.GetBack(building);
            var owner = _context.Users.FirstOrDefault(u => dbbuilding.OwnerId == u.Id);
            dbbuilding.OwnerId = owner?.Id;
            
            var phone = _context.Phones.FirstOrDefault(p => p.Id == dbbuilding.PhoneId);
            dbbuilding.PhoneId = phone?.Id;
            
            _context.Buildings.Add(dbbuilding);
            _context.SaveChanges();
            return new PostBuildingResponse { Building = _mapper.GetFront(dbbuilding) };
            
        }

        public GetBuildingResponse Get(Guid id)
        {
            return new GetBuildingResponse { Building = _mapper.GetFront(_context.Buildings.FirstOrDefault(b => b.Id == id))};
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
            var res = _context.Buildings.FirstOrDefault(b => b.Id == id);
            if(res == null)
                return new DeleteBuildingResponse { IsSuccess = false };

            _context.Buildings.Remove(res);
            _context.SaveChanges();
            return new DeleteBuildingResponse { IsSuccess = true };
        }

        public PutBuildingResponse Update(Building building)
        {
            _context.Buildings.Update(_mapper.GetBack(building));

            _context.SaveChanges();
            return new PutBuildingResponse(); 
        }
    }
}
