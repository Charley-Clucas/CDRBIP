using CDRBIP.Modules.CallDetailRecordManagement.Application.Generic;
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
        public class Query : IRequest<IEnumerable<CallCountAndDurationDto>>
        {
            public CallType? CallType { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<CallCountAndDurationDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<CallCountAndDurationDto>> Handle(Query request, CancellationToken cancellationToken)
            {

                return await _unitOfWork.CallDetailRecords.GetCallCountAndDuration(request.CallType, cancellationToken);
            }
        }
    }
}
