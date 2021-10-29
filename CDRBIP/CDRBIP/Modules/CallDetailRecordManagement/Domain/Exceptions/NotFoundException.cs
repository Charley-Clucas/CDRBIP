using System;

namespace CDRBIP.Modules.CallDetailRecordManagement.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(Type type, string id)
            : base($"{type.Name} with id {id} does not exist")
        {
        }
    }
}
