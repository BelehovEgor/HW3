using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAdapter.Commands
{
    public interface IGetUserCommand
    {
        User Execute(Guid id);
    }
}
