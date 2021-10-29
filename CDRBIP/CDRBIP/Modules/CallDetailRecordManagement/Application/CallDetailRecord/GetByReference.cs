using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public class GetByReference
    {
        public class Query : IRequest<Domain.CallDetailRecord>
        {
            public string Reference { get; set; }
        }

        public class Handler : IRequestHandler<Query, Domain.CallDetailRecord>
        {
            private readonly ICallDetailRecordRepository _callDetailRecordRepository;

            public Handler(ICallDetailRecordRepository callDetailRecordRepository)
            {
                _callDetailRecordRepository = callDetailRecordRepository;
            }

            public async Task<Domain.CallDetailRecord> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _callDetailRecordRepository.GetByReference(request.Reference, cancellationToken);
            }
        }
    }
}
