using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Data;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.ScheduleTasks;
using Nop.Core.Infrastructure;
using Nop.Services.Localization;
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

            // Schedule task kaydı
            var scheduleTaskService = scope.ServiceProvider.GetService<IScheduleTaskService>();
            if (scheduleTaskService != null)
            {
                var taskType = typeof(Tasks.TrialExpiryTask).FullName + ", Nop.Web";
                var existingTask = scheduleTaskService.GetTaskByTypeAsync(taskType).GetAwaiter().GetResult();
                if (existingTask == null)
                {
                    scheduleTaskService.InsertTaskAsync(new ScheduleTask
                    {
                        Name = "Trial Expiry Check",
                        Seconds = 86400,
                        Type = taskType,
                        Enabled = true,
                        StopOnError = false
                    }).GetAwaiter().GetResult();
                }
            }

            // Türkçe dil paketi import
            try
            {
                var languageService = scope.ServiceProvider.GetService<ILanguageService>();
                var localizationService = scope.ServiceProvider.GetService<ILocalizationService>();
                if (languageService == null || localizationService == null) return;

                var languages = languageService.GetAllLanguagesAsync(showHidden: true).GetAwaiter().GetResult();
                var trLanguage = languages.FirstOrDefault(l => l.LanguageCulture == "tr-TR");

                if (trLanguage == null)
                {
                    trLanguage = new Language
                    {
                        Name = "Türkçe",
                        LanguageCulture = "tr-TR",
                        UniqueSeoCode = "tr",
                        FlagImageFileName = "tr.png",
                        Rtl = false,
                        Published = true,
                        DisplayOrder = 1
                    };
                    languageService.InsertLanguageAsync(trLanguage).GetAwaiter().GetResult();
                }

                var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "language_pack.tr-TR.xml");
                if (!File.Exists(xmlPath)) return;

                using var reader = new StreamReader(xmlPath);
                localizationService.ImportResourcesFromXmlAsync(trLanguage, reader).GetAwaiter().GetResult();
            }
            catch
            {
                // Dil paketi yüklenemese bile uygulama çalışmaya devam eder
            }
        }

        public int Order => 1000;
    }
}
