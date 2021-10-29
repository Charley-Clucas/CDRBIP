using CDRBIP.Modules.CallDetailRecordManagement.Domain;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public class GetMostExpensiveCalls
    {
        public class Query : IRequest<List<Domain.CallDetailRecord>>
        {
            public long CallerId { get; set; }
            public int RequestedAmount { get; set; }
            public CallType? CallType { get; set; }
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
                return await _callDetailRecordRepository.GetMostExpensiveCalls(request.CallerId, request.RequestedAmount, request.CallType, cancellationToken);
            }
        }
    }
}
