using CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.Generic
{
    public interface IUnitOfWork
    {
        ICallDetailRecordRepository CallDetailRecords { get; }
        Task<int> CompleteAsync(CancellationToken cancellationToken);
    }
}
