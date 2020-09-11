using System.Linq;
using System.Threading.Tasks;
using Calendar.SQL;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Telegram.Bot;

namespace Calendar.TelegramMessage
{
	class TelegramMesageEvent : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			DataBaseContext BaseContext = new DataBaseContext();
			foreach(Event reminder in BaseContext._Events.Include("user"))
			{
				EventTime eventTime = BaseContext._EventTimes.Where(p => p._event == reminder).FirstOrDefault();
				if(eventTime != null)
				{
					if(eventTime.dateTime != null)
					{
						if(eventTime.dateTime.Date == System.DateTime.Today
								&& eventTime.dateTime.Hour == System.DateTime.Now.Hour
								&& eventTime.dateTime.Minute == System.DateTime.Now.Minute)
						{
							if(reminder.user.SendMessageTelegram == true)
							{
								TelegramBotClient BotClient = new TelegramBotClient("779815529:AAEQVsHABl-V1rNhxX3vDnCo_r1xQjYczNU");
								System.String busy = "";
								if(reminder.Busy == true)
								{
									busy = "Занят";
								}
								else
								{
									busy = "Не занят";
								}
								System.String name = "Название: " + reminder.Name + "\n";
								name += "Описание: " + reminder.Note + "\n";
								name += "Локация: " + reminder.Location + "\n";
								name += "Занятость: " + busy;
								await BotClient.SendTextMessageAsync(reminder.user.ID, name);
							}
						}
					}
				}
			}
			BaseContext.Dispose();
		}
	}
}
