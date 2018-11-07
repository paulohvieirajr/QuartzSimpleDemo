using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzSimpleDemo
{
    // Herda da Classe IJob
    public class DemoJob : IJob
    {
        // Implementa o método Execute
        public Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine($"DemoJob: {DateTime.Now}");
            Debug.WriteLine($"JobKey: {context.JobDetail.Key}");
            return Task.CompletedTask;
        }
    }
}
