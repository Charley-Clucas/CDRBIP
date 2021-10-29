using CDRBIP.Modules.CallDetailRecordManagement.Domain;
using CDRBIP.Modules.CallDetailRecordManagement.Domain.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public class GetCallCountAndDuration
    {
        public class Query : IRequest<List<CallCountAndDurationDto>>
        {
            public CallType? CallType { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<CallCountAndDurationDto>>
        {
            private readonly ICallDetailRecordRepository _callDetailRecordRepository;

            public Handler(ICallDetailRecordRepository callDetailRecordRepository)
            {
                _callDetailRecordRepository = callDetailRecordRepository;
            }

            public async Task<List<CallCountAndDurationDto>> Handle(Query request, CancellationToken cancellationToken)
            {

                return await _callDetailRecordRepository.GetCallCountAndDuration(request.CallType, cancellationToken);
            }
        }
    }
}
