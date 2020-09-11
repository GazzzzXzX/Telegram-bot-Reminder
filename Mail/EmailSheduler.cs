using Quartz;
using Quartz.Impl;

namespace Calendar.Mail
{
	class EmailScheduler
	{
		public static void Start()
		{
			ReminderStart();
			EventStart();
			PurposeStart();
		}

		private static async void ReminderStart()
		{
			IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
			await scheduler.Start();

			IJobDetail job = JobBuilder.Create<E_Mail>().Build();


			ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
				.WithIdentity("trigger1", "group1")     // идентифицируем триггер с именем и группой
				.StartNow()                            // запуск сразу после начала выполнения
				.WithSimpleSchedule(x => x            // настраиваем выполнение действия
					.WithIntervalInMinutes(1)          // через 1 минуту
					.RepeatForever())                   // бесконечное повторение
				.Build();                               // создаем триггер

			await scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
		}
		private static async void EventStart()
		{
			IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
			await scheduler.Start();

			IJobDetail job = JobBuilder.Create<E_MailEvent>().Build();


			ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
				.WithIdentity("trigger2", "group2")     // идентифицируем триггер с именем и группой
				.StartNow()                            // запуск сразу после начала выполнения
				.WithSimpleSchedule(x => x            // настраиваем выполнение действия
					.WithIntervalInMinutes(1)          // через 1 минуту
					.RepeatForever())                   // бесконечное повторение
				.Build();                               // создаем триггер

			await scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
		}
		private static async void PurposeStart()
		{
			IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
			await scheduler.Start();

			IJobDetail job = JobBuilder.Create<E_MailPurpose>().Build();


			ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
				.WithIdentity("trigger3", "group3")     // идентифицируем триггер с именем и группой
				.StartNow()                            // запуск сразу после начала выполнения
				.WithSimpleSchedule(x => x            // настраиваем выполнение действия
					.WithIntervalInMinutes(1)          // через 1 минуту
					.RepeatForever())                   // бесконечное повторение
				.Build();                               // создаем триггер

			await scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
		}
	}
}
