using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public class GetByReference
    {
        public class Query : IRequest<CallDetailRecordDto>
        {

        }

        public class Handler : IRequestHandler<Query, CallDetailRecordDto>
        {
            public async Task<CallDetailRecordDto> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        public class CallDetailRecordDto
        {

        }
    }
}
