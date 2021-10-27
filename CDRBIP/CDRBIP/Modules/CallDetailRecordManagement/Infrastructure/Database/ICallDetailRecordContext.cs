using CDRBIP.Modules.CallDetailRecordManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Infrastructure.Database
{
    public interface ICallDetailRecordContext
    {
        DbSet<CallDetailRecord> CallDetailRecords { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
