using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public class GetMostExpensiveCalls
    {
        public class Query : IRequest<IEnumerable<CallDetailRecordDto>>
        {

        }

        public class Handler : IRequestHandler<Query, IEnumerable<CallDetailRecordDto>>
        {
            public async Task<IEnumerable<CallDetailRecordDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        public class CallDetailRecordDto
        {

        }
    }
}
