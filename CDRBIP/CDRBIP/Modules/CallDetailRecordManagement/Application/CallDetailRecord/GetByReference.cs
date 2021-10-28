using CDRBIP.Modules.CallDetailRecordManagement.Application.Generic;
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
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Domain.CallDetailRecord> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.CallDetailRecords.GetByReference(request.Reference, cancellationToken);
            }
        }
    }
}
