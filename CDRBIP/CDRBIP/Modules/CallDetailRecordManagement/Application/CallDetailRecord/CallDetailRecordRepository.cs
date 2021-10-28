using CDRBIP.Modules.CallDetailRecordManagement.Domain;
using CDRBIP.Modules.CallDetailRecordManagement.Domain.Dtos;
using CDRBIP.Modules.CallDetailRecordManagement.Infrastructure.Database;
using CDRBIP.Modules.CallDetailRecordManagement.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public class CallDetailRecordRepository : ICallDetailRecordRepository
    {
        private readonly ICallDetailRecordContext _context;
        private DateTime timePeriodLimit;

        public CallDetailRecordRepository(ICallDetailRecordContext context)
        {
            _context = context;
            timePeriodLimit = DateTime.Today.AddDays(-30);
        }

        public async Task<IEnumerable<Domain.CallDetailRecord>> GetAllByCallerId(long callerId, CancellationToken cancellationToken)
        {
            var result = await _context.CallDetailRecords
                .Where(c => c.CallerId == callerId && c.CallDate >= timePeriodLimit)
                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<Domain.CallDetailRecord> GetByReference(string reference, CancellationToken cancellationToken)
        {
            var callDetailLogToReturn = await _context.CallDetailRecords
                .Where(c => c.Reference == reference)
                .SingleOrDefaultAsync(cancellationToken);

            if (callDetailLogToReturn == null)
            {
                throw new NotFoundException($"No Call Detail Log found with reference = {reference}");
            }

            return callDetailLogToReturn;
        }

        public async Task<IEnumerable<CallCountAndDurationDto>> GetCallCountAndDuration(CallType? callType, CancellationToken cancellationToken)
        {
            if (callType == null)
            {
                var resultWithoutFilter = await _context.CallDetailRecords
                    .Where(c => c.CallDate >= timePeriodLimit)
                    .GroupBy(c => c.CallerId)
                    .Select(x => new CallCountAndDurationDto
                    {
                        TotalCount = x.Count(),
                        TotalDuration = x.Select(y => y.Duration).Sum()
                    }).ToListAsync();

                return resultWithoutFilter;
            }

            var resultWithFilter = await _context.CallDetailRecords
                .Where(c => c.CallDate >= timePeriodLimit && c.Type == callType)
                .GroupBy(c => c.CallerId)
                .Select(x => new CallCountAndDurationDto
                {
                    TotalCount = x.Count(),
                    TotalDuration = x.Select(y => y.Duration).Sum()
                }).ToListAsync();

            return resultWithFilter;
        }

        public async Task<IEnumerable<Domain.CallDetailRecord>> GetMostExpensiveCalls(long callerId, int requestedAmount, CallType? callType, CancellationToken cancellationToken)
        {
            //No filter provided
            if (callType == null)
            {
                return await _context.CallDetailRecords
                    .Where(c => c.CallerId == callerId && c.CallDate >= timePeriodLimit)
                    .OrderByDescending(c => c.Cost)
                    .Take(requestedAmount)
                    .ToListAsync(cancellationToken);
            }

            //Filter provided
            return await _context.CallDetailRecords
                    .Where(c => c.CallerId == callerId && c.CallDate >= timePeriodLimit && c.Type == callType)
                    .OrderByDescending(c => c.Cost)
                    .Take(requestedAmount)
                    .ToListAsync(cancellationToken);
        }
    }
}
