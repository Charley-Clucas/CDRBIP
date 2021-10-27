using CDRBIP.Modules.CallDetailRecordManagement.Infrastructure.Database;
using CDRBIP.Modules.CallDetailRecordManagement.Infrastructure.Helpers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public class Create
    {
        public class Command : IRequest<int>
        {
            public string CallDetailRecord { get; set; }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly ICallDetailRecordContext _callDetailRecordContext;

            public Handler(ICallDetailRecordContext callDetailRecordContext)
            {
                _callDetailRecordContext = callDetailRecordContext;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var jsonConvertor = new ConvertCsvToJson();

                var convertedJsonString = await jsonConvertor.ConvertAsync(request.CallDetailRecord);

                var callDetailRecordObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Domain.CallDetailRecord>>(convertedJsonString);

                await _callDetailRecordContext.CallDetailRecords.AddRangeAsync(callDetailRecordObjects, cancellationToken);

                return await _callDetailRecordContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
