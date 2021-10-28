﻿using CDRBIP.Modules.CallDetailRecordManagement.Application.Generic;
using CDRBIP.Modules.CallDetailRecordManagement.Domain;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public class GetMostExpensiveCalls
    {
        public class Query : IRequest<IEnumerable<Domain.CallDetailRecord>>
        {
            public long CallerId { get; set; }
            public int RequestedAmount { get; set; }
            public CallType? CallType { get; set; }
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
                return await _unitOfWork.CallDetailRecords.GetMostExpensiveCalls(request.CallerId, request.RequestedAmount, request.CallType, cancellationToken);
            }
        }
    }
}
