using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Data;
using Nop.Core.Domain.ScheduleTasks;
using Nop.Core.Infrastructure;
using Nop.Services.ScheduleTasks;

namespace Nop.Web.Infrastructure
{
    public class TrialTaskStartup : INopStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
        }

        public void Configure(IApplicationBuilder application)
        {
            if (!DataSettingsManager.IsDatabaseInstalled())
                return;

            var serviceProvider = application.ApplicationServices;
            using var scope = serviceProvider.CreateScope();
            var scheduleTaskService = scope.ServiceProvider.GetService<IScheduleTaskService>();
            if (scheduleTaskService == null) return;

            var taskType = typeof(Tasks.TrialExpiryTask).FullName + ", Nop.Web";
            var existingTask = scheduleTaskService.GetTaskByTypeAsync(taskType).GetAwaiter().GetResult();
            if (existingTask != null) return;

            scheduleTaskService.InsertTaskAsync(new ScheduleTask
            {
                Name = "Trial Expiry Check",
                Seconds = 86400, // Günde bir
                Type = taskType,
                Enabled = true,
                StopOnError = false
            }).GetAwaiter().GetResult();
        }

        public int Order => 1000;
    }
}
