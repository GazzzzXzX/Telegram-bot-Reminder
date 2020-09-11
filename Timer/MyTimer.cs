using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Calendar.OrganizerTelegram;
using Calendar.SQL;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.Timer
{
	class MyTimer
	{
		System.Timers.Timer t = new System.Timers.Timer();
		event ElapsedEventHandler timerElapsed;

		public event ElapsedEventHandler Elapsed
		{
			add
			{
				t.Elapsed += value;
				timerElapsed += value;
			}
			remove
			{
				t.Elapsed -= value;
				timerElapsed -= value;
			}
		}

		public System.Double Interval
		{
			get
			{
				return t.Interval;
			}
			set
			{
				t.Interval = value;
			}
		}

		public System.Object Statistic { get; private set; }

		public void StartWork(TelegramBotClient BotClient, System.Object message, InlineButtonOrganizer inlineButton, User users, DataBaseContext baseContext)
		{
			t.Elapsed += (sender, e) => T_Elapsed(sender, e, BotClient, message, inlineButton, users, baseContext);
			users.TimeWork = true;
			
			t.Start();
			baseContext.SaveChanges();
		}


		public void StartRelax(TelegramBotClient BotClient, System.Object message, InlineButtonOrganizer inlineButton, User users, DataBaseContext baseContext)
		{
			t.Elapsed += (sender, e) => T_Elapsed1(sender, e, BotClient, message, inlineButton, users, baseContext);

			users.TimeWork = true;
			
			t.Start();
			baseContext.SaveChanges();
		}

		private async void T_Elapsed(System.Object sender, ElapsedEventArgs e, TelegramBotClient BotClient, System.Object message, InlineButtonOrganizer inlineButton, User users, DataBaseContext baseContext)
		{
			t.Stop();
			
			CallbackQuery _message = message as CallbackQuery;
			await BotClient.SendTextMessageAsync
				(
				_message.From.Id, 
				"Когда в работе успех, и в дотку сгонять не грех!\nПора отдыхать.", //Когда в работе успех, и перекур не грех.
				replyMarkup: inlineButton.StartOrganizerTimeRelax
				);

			Statistics stat = baseContext._Statistics.Where(p => p.user == users).FirstOrDefault(p => p.Day == System.DateTime.Today);
			if(stat == null)
			{
				baseContext._Statistics.Add(new Statistics()
				{
					Day = System.DateTime.Today,
					user = users,
					TimeInDayToWork = users.TimeToWork
				});
			}
			else
			{
				stat.TimeInDayToWork += users.TimeToWork;
			}
			baseContext.SaveChanges();
		}

		private async void T_Elapsed1(System.Object sender, ElapsedEventArgs e, TelegramBotClient BotClient, System.Object message, InlineButtonOrganizer inlineButton, User users, DataBaseContext baseContext)
		{
			t.Stop();
			
			CallbackQuery _message = message as CallbackQuery;
			await BotClient.SendTextMessageAsync
				(
				_message.From.Id,
				"Пригорел?\nПора работать!",//Работе время, а досугу час.
				replyMarkup: inlineButton.StartOrganizerTime
				);

			Statistics stat = baseContext._Statistics.Where(p => p.user == users).FirstOrDefault(p => p.Day == System.DateTime.Today);
			if(stat == null)
			{
				baseContext._Statistics.Add(new Statistics()
				{
					Day = System.DateTime.Today,
					user = users,
					TimeInDayToRelax = users.TimeToRelaxation
				});
			}
			else
			{
				stat.TimeInDayToRelax += users.TimeToRelaxation;
			}
			baseContext.SaveChanges();
		}
	


		public void Stop()
		{
			t.Stop();
		}

		public void RaiseElapsed()
		{
			if(timerElapsed != null)
			{
				timerElapsed(null, null);
			}
		}
	}
}
