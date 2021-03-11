using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAdapter.Commands
{
    public interface IDeleteCommand
    {
        bool Execute(Guid id);
    }
}
