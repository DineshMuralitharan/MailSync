using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSync.Models
{
    /// <summary>
    /// Create class for email scheduler
    /// </summary>
    public class EmailScheduler
    {
        /// <summary>
        /// To start the schedule
        /// </summary>
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<EmailModel>().Build();

            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("EmailModel", "EmailSchedule")
            .StartNow()
            .WithSimpleSchedule(x => x
            .WithIntervalInMinutes(5)
            .RepeatForever())
            .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}