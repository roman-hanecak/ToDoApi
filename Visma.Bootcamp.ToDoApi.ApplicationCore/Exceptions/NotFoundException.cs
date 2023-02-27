using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        { }
    }
}