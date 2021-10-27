using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public class GetCallDuration
    {
        public class Query : IRequest<CallDurationDto>
        {

        }

        public class Handler : IRequestHandler<Query, CallDurationDto>
        {
            public async Task<CallDurationDto> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        public class CallDurationDto
        {

        }
    }
}
