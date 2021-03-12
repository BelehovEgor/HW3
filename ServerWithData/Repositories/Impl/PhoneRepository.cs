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
    public class PhoneRepository : IPhoneRepository
    {
        private AppContext _context;
        private IMapper<Phone, DbPhone> _mapper;

        public PhoneRepository([FromServices] AppContext context, [FromServices] IMapper<Phone, DbPhone> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public PostPhoneResponse Add(Phone phone)
        {
            var res = _context.Phones.FirstOrDefault(p => p.Id == phone.Id);
            if (res != null)
                return new PostPhoneResponse { Phone = _mapper.GetFront(res) };
            
            var dbPhone = _mapper.GetBack(phone);
            _context.Phones.Add(dbPhone);
            //if (dbPhone.BuildingId != null) {
            //    var building = _context.Buildings.FirstOrDefault(b => b.Id == dbPhone.BuildingId);
            //    if (building != null)
            //        building.PhoneId = dbPhone.Id;
            //    else
            //        dbPhone.BuildingId = null;
            //}
            _context.SaveChanges();
            return new PostPhoneResponse { Phone = phone };
        }

        public GetPhoneResponse Get(Guid id)
        {
            return new GetPhoneResponse { Phone = _mapper.GetFront(_context.Phones.Where(p => p.Id == id).FirstOrDefault()) };
        }

        public GetPhonesResponse GetAll()
        {
            var answer = new GetPhonesResponse { Phones = new List<Phone>() };
            foreach (var dbphone in _context.Phones)
            {
                answer.Phones.Add(_mapper.GetFront(dbphone));
            }
            return answer;
        }

        public DeletePhoneResponse Remove(Guid id)
        {
            var dbphone = _context.Phones.FirstOrDefault(p => p.Id == id);
            if (dbphone == null)
                return new DeletePhoneResponse { IsSuccess = false };

            _context.Phones.Remove(dbphone);
            _context.SaveChanges();
            return new DeletePhoneResponse { IsSuccess = true };
        }

        public PutPhoneResponse Update(Phone phone)
        {
            _context.Phones.Update(_mapper.GetBack(phone));
            _context.SaveChanges();

            return new PutPhoneResponse();
        }
    }
}
