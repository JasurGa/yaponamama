using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.Common.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string name, object key)
            : base($"Entity \"{name}\" ({key}) already exists.") { }
    }
}
