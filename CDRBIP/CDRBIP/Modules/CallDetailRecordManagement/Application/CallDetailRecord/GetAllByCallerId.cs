using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public class GetAllByCallerId
    {
        public class Query : IRequest<IEnumerable<Domain.CallDetailRecord>>
        {

        }

        public class Handler : IRequestHandler<Query, IEnumerable<Domain.CallDetailRecord>>
        {
            public async Task<IEnumerable<Domain.CallDetailRecord>> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        public class CallDetailRecordDto
        {

        }
    }
}
