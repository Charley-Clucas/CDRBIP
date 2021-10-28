using CDRBIP.Modules.CallDetailRecordManagement.Application.Generic;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public class GetAllByCallerId
    {
        public class Query : IRequest<IEnumerable<Domain.CallDetailRecord>>
        {
            public long CallerId { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<Domain.CallDetailRecord>>
        {
            private readonly IUnitOfWork _unitOfWork;


            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Domain.CallDetailRecord>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.CallDetailRecords.GetAllByCallerId(request.CallerId, cancellationToken);
            }
        }
    }
}
