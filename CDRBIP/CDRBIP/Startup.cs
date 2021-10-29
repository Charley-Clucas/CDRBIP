using CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord;
using CDRBIP.Modules.CallDetailRecordManagement.Domain.Context;
using CDRBIP.Modules.CallDetailRecordManagement.Infrastructure.Database;
using CDRBIP.Modules.CallDetailRecordManagement.Infrastructure.Processing;
using CDRBIP.Modules.CallDetailRecordManagement.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

namespace CDRBIP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private string _connectionString { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            _connectionString = Configuration.GetConnectionString("CDRBIP");

            services.AddControllers();

            services.AddQuartz(q =>
            {
                q.SchedulerId = "CDR-Quartz";
                q.SchedulerName = "Quartz CDRBIP Scheduler";
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.UseInMemoryStore();
                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 10;
                });

                q.AddJob<RetrieveCDRFileJob>(j => j.WithIdentity("RetrieveCDRFileJob"));
                q.AddTrigger(t => t
                    .WithIdentity("RetrieveCDRFileTrigger")
                    .ForJob("RetrieveCDRFileJob")
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever()));

            });

            services.AddScoped<ICallDetailRecordContext, CallDetailRecordContext>();
            services.AddScoped<ICallDetailRecordRepository, CallDetailRecordRepository>();
            services.AddScoped<ICallDetailRecordDownloadService, CallDetailRecordDownloadService>();
            services.AddTransient<RetrieveCDRFileJob>();
            services.AddMediatR(typeof(Startup).Assembly)
                .AddDbContext<CallDetailRecordContext>(options => options.UseInMemoryDatabase(databaseName: "CDRBIP"));

            services.AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
