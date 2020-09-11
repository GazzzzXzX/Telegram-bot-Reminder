using Calendar.Command;
using Telegram.Bot;
using Telegram.Bot.Types;
using Calendar.SQL;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Calendar.ItemCalendar
{
	class PurposeCal : IEventCal
	{
		public System.String Name { get; set; }

		public async void SetContext(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			System.Console.WriteLine("Содержание!");

			Purpose purpose = baseContext._Purposes.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			purpose.Note = _message.Text;

		
			await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nСодержание: " + _message.Text, replyMarkup: inlineButton.AddPurpose);

			baseContext.SaveChanges();
		}

		public async void SetName(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, User users, DataBaseContext baseContext)
		{
			users.Work = 2700;

			Purpose purpose = baseContext._Purposes.Where(p => p.user == users).FirstOrDefault(p => p.Work == true);
			if (purpose != null)
			{
				purpose.Work = false;
			}

			baseContext._Purposes.Add(new Purpose() { Name = Name, user = users, Work = true });
			await BotClient.SendTextMessageAsync(_message.Chat.Id, "Заголовок: " + Name, replyMarkup: inlineButton.AddPurpose);
			baseContext.SaveChanges();
			SetTimeDefoult(users, baseContext);
		}

		public async void SetTime(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, User users, DataBaseContext baseContext)
		{
			TempDate tempDate = baseContext._TempDate.Where(p => p.user == users).FirstOrDefault();
			try
			{
				System.TimeSpan timeSpan = tempDate.date.TimeOfDay;
				tempDate.date -= timeSpan;
				System.TimeSpan time = System.TimeSpan.Parse(_message.Text);
				tempDate.date = tempDate.date.Date + time;

				System.Console.WriteLine(tempDate.date);

				Purpose purpose = baseContext._Purposes.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
				EventTime eventTime = baseContext._EventTimes.Where(p => p.purpose.ID == purpose.ID).FirstOrDefault();
				if(eventTime.Work == true)
				{
					baseContext._EventTimes.Add(new EventTime() { purpose = purpose, dateTime = tempDate.date, Work = true });
				}
				else
				{
					eventTime.dateTime = tempDate.date;
					eventTime.Work = true;
				}
			
				await BotClient.SendTextMessageAsync(_message.Chat.Id, "Заголовок: " + Name, replyMarkup: inlineButton.AddPurpose);
				baseContext.SaveChanges();
			}
			catch
			{
				System.Console.WriteLine("RemiderCal.cs - SetTime - Time");
			}

		}

		private void SetTimeDefoult(User users, DataBaseContext baseContext)
		{
			TempDate tempDate = baseContext._TempDate.Where(p => p.user == users).FirstOrDefault();
			System.TimeSpan timeSpan = tempDate.date.TimeOfDay;
			tempDate.date -= timeSpan;

			System.TimeSpan time = System.TimeSpan.Parse("9:00");
			tempDate.date = tempDate.date + time;

			System.Console.WriteLine(tempDate.date);

			Purpose purpose = baseContext._Purposes.Where(p => p.user == users).FirstOrDefault(p => p.Work == true);

			baseContext._EventTimes.Add(new EventTime() { purpose = purpose, dateTime = tempDate.date, Work = false });

			baseContext.SaveChanges();
		}

		public void SetFalse(CallbackQuery _message, DataBaseContext baseContext)
		{
			foreach(Purpose temp in baseContext._Purposes.Include("user"))
			{
				if(temp.user.ID == _message.From.Id && temp.Work == true)
				{
					temp.Work = false;
				}
			}
			baseContext.SaveChanges();
		}

		public void SetLocation(TelegramBotClient BotClient, Message message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			
		}

		public async void ShowItem(TelegramBotClient BotClient, System.Object message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			Name = _message.Data;
			System.String[] words = Name.Split(new System.Char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
			System.Int32 temp = System.Convert.ToInt32(words[0]);

			Purpose purpose = baseContext._Purposes.Where(p => p.ID == temp).FirstOrDefault();
			EventTime eventTime = baseContext._EventTimes.Where(p => p.purpose == purpose).FirstOrDefault();
			purpose.Work = true;

			baseContext.SaveChanges();

			await BotClient.EditMessageTextAsync
				(
				_message.From.Id,
				_message.Message.MessageId,
				"Цель: " + purpose.Name + "\nОписание: " + purpose.Note + "\nПродолжительность: "
				+ purpose.Duration.Hours.ToString() + ":" + purpose.Duration.Minutes.ToString() + "\nВремя: " + eventTime.dateTime.TimeOfDay,
				replyMarkup: inlineButton.ChangePurpose
				);
		}

		public async void ChangeName(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			Purpose purpose = baseContext._Purposes.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			purpose.Name = _message.Text;

			await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nСодержание: " + _message.Text, replyMarkup: inlineButton.ChangePurpose);

			baseContext.SaveChanges();
		}

		public async void ChangeNote(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			Purpose purpose = baseContext._Purposes.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			purpose.Note = _message.Text;

			await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nСодержание: " + _message.Text, replyMarkup: inlineButton.ChangePurpose);

			baseContext.SaveChanges();
		}

		public void Dalete(CallbackQuery _message, DataBaseContext baseContext)
		{
			TempDate tempDate = baseContext._TempDate.Where(p => p.user.ID == _message.From.Id).FirstOrDefault();
			Purpose purpose = baseContext._Purposes.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);

			foreach(EventTime eventTime in baseContext._EventTimes.Include("_event"))
			{
				if(eventTime.reminder != null)
				{
					if(eventTime.reminder.ID == purpose.ID)
					{
						if(eventTime.dateTime.Date == tempDate.date.Date)
						{
							baseContext._EventTimes.Remove(eventTime);
						}
					}
				}
			}
			baseContext._Purposes.Remove(purpose);
			baseContext.SaveChanges();
		}

		public async void ChangeTime(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			Purpose purpose = baseContext._Purposes.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p.purpose.ID == purpose.ID).FirstOrDefault();
			try
			{
				System.TimeSpan time = System.TimeSpan.Parse(_message.Text);
				System.TimeSpan timeSpan = eventTime.dateTime.TimeOfDay;
				eventTime.dateTime -= timeSpan;
				eventTime.dateTime = eventTime.dateTime.Date + time;

				await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nВремя изменено: " + _message.Text, replyMarkup: inlineButton.ChangePurpose);
			}
			catch
			{
				await BotClient.SendTextMessageAsync(_message.Chat.Id, "Время введено неверно: " + _message.Text, replyMarkup: inlineButton.ChangePurpose);
			}
			baseContext.SaveChanges();
		}

		public void ChangeLocation(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{

		}
	}
}
