using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl;

namespace QuartzSimpleDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            StartScheduler();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private void StartScheduler()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = factory.GetScheduler().Result;
            scheduler.Start().Wait();
            ScheduleJobs(scheduler);
        }

        private void ScheduleJobs(IScheduler scheduler)
        {
            // Defino meu joba ser executado. 
            IJobDetail job = JobBuilder.Create<DemoJob>().WithIdentity("demoJob", "demoGroup").Build();
            IJobDetail job2 = JobBuilder.Create<DemoJob2>().WithIdentity("demoJob2", "demoGroup2").Build();

            // Trigger para que o job seja executado imediatamente, e repetidamente a cada 60 segundos
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("demoTrigger", "demoGroup")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(60)
                  .RepeatForever())
              .Build();

            ITrigger trigger2 = TriggerBuilder.Create()
              .WithIdentity("demoTrigger2", "demoGroup2")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(60)
                  .RepeatForever())
              .Build();

            scheduler.ScheduleJob(job, trigger).Wait();
            scheduler.ScheduleJob(job2, trigger2).Wait();
        }
    }
}
