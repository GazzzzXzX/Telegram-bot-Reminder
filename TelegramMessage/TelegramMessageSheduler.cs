using Quartz;
using Quartz.Impl;

namespace Calendar.TelegramMessage
{
	class TelegramMessageSheduler
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

			IJobDetail job = JobBuilder.Create<TelegramMesageReminder>().Build();


			ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
				.WithIdentity("trigger4", "group4")     // идентифицируем триггер с именем и группой
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

			IJobDetail job = JobBuilder.Create<TelegramMesageEvent>().Build();


			ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
				.WithIdentity("trigger5", "group5")     // идентифицируем триггер с именем и группой
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

			IJobDetail job = JobBuilder.Create<TelegramMesagePurpose>().Build();


			ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
				.WithIdentity("trigger6", "group6")     // идентифицируем триггер с именем и группой
				.StartNow()                            // запуск сразу после начала выполнения
				.WithSimpleSchedule(x => x            // настраиваем выполнение действия
					.WithIntervalInMinutes(1)          // через 1 минуту
					.RepeatForever())                   // бесконечное повторение
				.Build();                               // создаем триггер

			await scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
		}
	}
}
