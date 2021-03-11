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
            var res = _context.Phones.Where(p => p.Id == phone.Id).FirstOrDefault();
            if (res == null)
            {
                var dbPhone = _mapper.GetBack(phone);
                _context.Phones.Add(dbPhone);
                _context.SaveChanges();
                return new PostPhoneResponse { Phone = phone };
            }
            return new PostPhoneResponse { Phone = _mapper.GetFront(res) };
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
            var dbphone = _context.Phones.Where(p => p.Id == id).FirstOrDefault();
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
