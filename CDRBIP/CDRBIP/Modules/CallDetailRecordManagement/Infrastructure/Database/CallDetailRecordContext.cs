using CDRBIP.Modules.CallDetailRecordManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace CDRBIP.Modules.CallDetailRecordManagement.Infrastructure.Database
{
    public class CallDetailRecordContext : DbContext, ICallDetailRecordContext
    {
        public CallDetailRecordContext()
        {
        }

        public CallDetailRecordContext(DbContextOptions<CallDetailRecordContext> options)
            :base(options)
        {
        }
       
        //Entities
        public DbSet<CallDetailRecord> CallDetailRecords { get; set; }

    }
}
