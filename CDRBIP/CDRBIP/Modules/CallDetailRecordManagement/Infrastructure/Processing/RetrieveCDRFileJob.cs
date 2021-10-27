using CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Infrastructure.Processing
{
    [DisallowConcurrentExecution]
    public class RetrieveCDRFileJob : IJob
    {
        private readonly IServiceProvider _provider;

        public RetrieveCDRFileJob(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _provider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new RetrieveCDRFile.Command());
            }
        }

    }
}
