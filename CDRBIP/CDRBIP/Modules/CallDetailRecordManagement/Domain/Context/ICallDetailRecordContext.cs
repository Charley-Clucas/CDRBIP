using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Domain.Context
{
    public interface ICallDetailRecordContext
    {
        DbSet<CallDetailRecord> CallDetailRecords { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
