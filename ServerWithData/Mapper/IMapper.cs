using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.Mapper
{
    public interface IMapper<Front, Back>
    {
        Back GetBack(Front front);
        Front GetFront(Back back);
    }
}
