using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using PROnline.Models.Files;

namespace PROnline.Src
{
    public class Task
    {
        ISchedulerFactory schedFact = new StdSchedulerFactory();
        public void StartTask()
        {
            IScheduler isch = schedFact.GetScheduler();
            isch.Start();

            JobDetail jd = new JobDetail("fileJob", null, typeof(FileTemp));
            Trigger trigger = TriggerUtils.MakeMinutelyTrigger(1);
            trigger.StartTimeUtc = TriggerUtils.GetEvenMinuteDate(DateTime.UtcNow);
            trigger.Name = "JobTrigger";
            isch.ScheduleJob(jd,trigger);
        }
    }
}