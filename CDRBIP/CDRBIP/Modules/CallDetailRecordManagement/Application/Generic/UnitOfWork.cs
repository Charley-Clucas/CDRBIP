using CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord;
using CDRBIP.Modules.CallDetailRecordManagement.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.Generic
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICallDetailRecordContext _context;

        public UnitOfWork(ICallDetailRecordContext context)
        {
            _context = context;
            CallDetailRecords = new CallDetailRecordRepository(_context);
        }

        public ICallDetailRecordRepository CallDetailRecords { get; set; }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
