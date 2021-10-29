using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public class GetAllByCallerId
    {
        public class Query : IRequest<List<Domain.CallDetailRecord>>
        {
            public long CallerId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Domain.CallDetailRecord>>
        {
            private readonly ICallDetailRecordRepository _callDetailRecordRepository;


            public Handler(ICallDetailRecordRepository callDetailRecordRepository)
            {
                _callDetailRecordRepository = callDetailRecordRepository;
            }

            public async Task<List<Domain.CallDetailRecord>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _callDetailRecordRepository.GetAllByCallerId(request.CallerId, cancellationToken);

                return result;
            }
        }
    }
}
