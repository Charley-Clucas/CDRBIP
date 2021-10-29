using CDRBIP.Modules.CallDetailRecordManagement.Domain;
using CDRBIP.Modules.CallDetailRecordManagement.Domain.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public interface ICallDetailRecordRepository
    {
        Task<List<Domain.CallDetailRecord>> GetAllByCallerId(long callerId, CancellationToken cancellationToken);

        Task<Domain.CallDetailRecord> GetByReference(string reference, CancellationToken cancellationToken);

        Task<List<Domain.CallDetailRecord>> GetMostExpensiveCalls(long callerId, int requestedAmount, CallType? callType, CancellationToken cancellationToken);

        Task<List<CallCountAndDurationDto>> GetCallCountAndDuration(CallType? callType, CancellationToken cancellationToken);

        Task<IEnumerable<Domain.CallDetailRecord>> SaveCDRFileRecords(IEnumerable<Domain.CallDetailRecord> recordsToInsert, CancellationToken cancellationToken);
    }
}
