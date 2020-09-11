using Calendar.Command;
using Telegram.Bot;
using Telegram.Bot.Types;
using Calendar.SQL;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Calendar.ItemCalendar
{
	class ReminderCal : IEventCal
	{
		public System.String Name { get; set; }

		public async void SetContext(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			System.Console.WriteLine("Содержание - Напоминание!");

			Reminder reminder = baseContext._Reminders.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			reminder.Note = _message.Text;
		
			await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nСодержание: " + _message.Text, replyMarkup: inlineButton.AddReminder);

			baseContext.SaveChanges();
		}

		public async void SetName(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, User users, DataBaseContext baseContext)
		{
			users.Work = 2600;

			Reminder reminder = baseContext._Reminders.Where(p => p.user == users).FirstOrDefault(p => p.Work == true);
			if(reminder != null)
			{
				reminder.Work = false;
			}

			baseContext._Reminders.Add(new Reminder() { Name = Name, user = users, Work = true });
			await BotClient.SendTextMessageAsync(_message.Chat.Id, "Заголовок: " + Name, replyMarkup: inlineButton.AddReminder);
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
				foreach(Reminder temp in baseContext._Reminders.Include("user"))
				{
					if(temp.Work == true)
					{
						EventTime eventTime = baseContext._EventTimes.Where(p => p.reminder.ID == temp.ID).FirstOrDefault();
						if(eventTime.Work == true)
						{
							baseContext._EventTimes.Add(new EventTime() { reminder = temp, dateTime = tempDate.date, Work = true });
						}
						else
						{
							eventTime.dateTime = tempDate.date;
							eventTime.Work = true;
						}
					}
				}
				await BotClient.SendTextMessageAsync(_message.Chat.Id, "Заголовок: " + Name, replyMarkup: inlineButton.AddReminder);
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
			tempDate.date += time;

			System.Console.WriteLine(tempDate.date);

			Reminder reminder = baseContext._Reminders.Where(p => p.user == users).FirstOrDefault(p => p.Work == true);

			baseContext._EventTimes.Add(new EventTime() { reminder = reminder, dateTime = tempDate.date, Work = false });
			baseContext.SaveChanges();
		}

		public void SetFalse(CallbackQuery _message, DataBaseContext baseContext)
		{
			foreach(Reminder temp in baseContext._Reminders.Include("user"))
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

			Reminder reminder = baseContext._Reminders.Where(p => p.ID == temp).FirstOrDefault();
			EventTime eventTime = baseContext._EventTimes.Where(p => p.reminder == reminder).FirstOrDefault();
			reminder.Work = true;

			baseContext.SaveChanges();
			try
			{
				await BotClient.EditMessageTextAsync
					(
					_message.From.Id,
					_message.Message.MessageId,
					"Название: " + reminder.Name + "\nОписание: " + reminder.Note + "\nВремя: " + eventTime.dateTime.TimeOfDay,
					replyMarkup: inlineButton.ChangeReminder
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Exception: ReminderCal : IEventCal - " + ex);
			}
		}

		public async void ChangeName(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			Reminder reminder = baseContext._Reminders.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			reminder.Name = _message.Text;

			await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nНазвание: " + _message.Text, replyMarkup: inlineButton.ChangeReminder);

			baseContext.SaveChanges();
		}

		public async void ChangeNote(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			Reminder reminder = baseContext._Reminders.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			reminder.Note = _message.Text;

			await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nСодержание: " + _message.Text, replyMarkup: inlineButton.ChangeReminder);

			baseContext.SaveChanges();
		}

		public void Dalete(CallbackQuery _message, DataBaseContext baseContext)
		{
			TempDate tempDate = baseContext._TempDate.Where(p => p.user.ID == _message.From.Id).FirstOrDefault();
			Reminder reminder = baseContext._Reminders.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);

			foreach(EventTime eventTime in baseContext._EventTimes.Include("reminder"))
			{
				if(eventTime.reminder != null)
				{
					if(eventTime.reminder.ID == reminder.ID)
					{
						if(eventTime.dateTime.Date == tempDate.date.Date)
						{
							baseContext._EventTimes.Remove(eventTime);
						}
					}
				}
			}
			baseContext._Reminders.Remove(reminder);
			baseContext.SaveChanges();
		}

		public async void ChangeTime(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			Reminder reminder = baseContext._Reminders.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p.reminder.ID == reminder.ID).FirstOrDefault();
			try
			{
				System.TimeSpan time = System.TimeSpan.Parse(_message.Text);
				System.TimeSpan timeSpan = eventTime.dateTime.TimeOfDay;
				eventTime.dateTime -= timeSpan;
				eventTime.dateTime = eventTime.dateTime.Date + time;

				await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nВремя изменено: " + _message.Text, replyMarkup: inlineButton.ChangeReminder);
			}
			catch
			{
				await BotClient.SendTextMessageAsync(_message.Chat.Id, "Время введено неверно: " + _message.Text, replyMarkup: inlineButton.ChangeReminder);
			}
			baseContext.SaveChanges();

		}

		public void ChangeLocation(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{

		}
	}
}
