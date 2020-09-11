using Calendar.Command;
using Telegram.Bot;
using Telegram.Bot.Types;
using Calendar.SQL;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Calendar.ItemCalendar
{
	class EventCal : IEventCal
	{
		public System.String Name { get; set; }

		public async void SetContext(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			System.Console.WriteLine("Содержание!");

			Event _event = baseContext._Events.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			_event.Note = _message.Text;

			if(_event.Busy == true)
			{
				await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nСодержание: " + _message.Text, replyMarkup: inlineButton.AddEventNotBusy);
			}
			else
			{
				await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nСодержание: " + _message.Text, replyMarkup: inlineButton.AddEventBusy);
			}

			baseContext.SaveChanges();
		}

		public async void SetName(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, User users, DataBaseContext baseContext)
		{
			users.Work = 2800;

			Event _event = baseContext._Events.Where(p => p.user == users).FirstOrDefault(p => p.Work == true);
			if(_event != null)
			{
				_event.Work = false;
			}

			baseContext._Events.Add(new Event() { Name = Name, user = users, Work = true, Busy = true });
			await BotClient.SendTextMessageAsync(_message.Chat.Id, "Заголовок: " + Name, replyMarkup: inlineButton.AddEventNotBusy);
			baseContext.SaveChanges();
			SetTimeDefoult(users, baseContext);
		}

		public async void SetTime(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, User users, DataBaseContext baseContext)
		{
			TempDate tempDate = baseContext._TempDate.Where(p => p.user == users).FirstOrDefault();
			Event _event = baseContext._Events.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p._event.ID == _event.ID).FirstOrDefault();
			try
			{
				System.TimeSpan time = System.TimeSpan.Parse(_message.Text);
				System.TimeSpan timeSpan = tempDate.date.TimeOfDay;
				tempDate.date -= timeSpan;
				tempDate.date = tempDate.date.Date + time;

				System.Console.WriteLine(tempDate.date);

				if(eventTime.Work == true)
				{
					baseContext._EventTimes.Add(new EventTime() { _event = _event, dateTime = tempDate.date, Work = true });
				}
				else
				{
					eventTime.dateTime = tempDate.date;
					eventTime.Work = true;
				}

				if(_event.Busy == true)
				{
					await BotClient.SendTextMessageAsync(_message.Chat.Id, "Время: " + _message.Text, replyMarkup: inlineButton.AddEventNotBusy);
				}
				else
				{
					await BotClient.SendTextMessageAsync(_message.Chat.Id, "Время: " + _message.Text, replyMarkup: inlineButton.AddEventBusy);
				}
				
			}
			catch
			{
				
					await BotClient.SendTextMessageAsync(_message.Chat.Id, "Время введено неверно: " + _message.Text, replyMarkup: inlineButton.BackToCalendar);
				
			}
			baseContext.SaveChanges();
		}

		private void SetTimeDefoult(User users, DataBaseContext baseContext)
		{
			TempDate tempDate = baseContext._TempDate.Where(p => p.user == users).FirstOrDefault();
			System.TimeSpan timeSpan = tempDate.date.TimeOfDay;
			tempDate.date -= timeSpan;
			System.TimeSpan time = System.TimeSpan.Parse("9:00");
			tempDate.date = tempDate.date + time;

			System.Console.WriteLine(tempDate.date);

			Event _event = baseContext._Events.Where(p => p.user == users).FirstOrDefault(p => p.Work == true);

			baseContext._EventTimes.Add(new EventTime() { _event = _event, dateTime = tempDate.date, Work = false });

			baseContext.SaveChanges();
		}

		public void SetFalse(CallbackQuery _message, DataBaseContext baseContext)
		{
			Event _event = baseContext._Events.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			_event.Work = false;

			baseContext.SaveChanges();
		}

		public async void SetLocation(TelegramBotClient BotClient, Message message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			Event _event = baseContext._Events.Where(p => p.user.ID == message.From.Id).FirstOrDefault(p => p.Work == true);
			_event.Location = message.Text;

			if(_event.Busy == true)
			{
				await BotClient.SendTextMessageAsync(message.Chat.Id, "Место проведения: " + message.Text, replyMarkup: inlineButton.AddEventNotBusy);
			}
			else
			{
				await BotClient.SendTextMessageAsync(message.Chat.Id, "Место проведения: " + message.Text, replyMarkup: inlineButton.AddEventBusy);
			}
			baseContext.SaveChanges();
		}

		public async  void ShowItem(TelegramBotClient BotClient, System.Object message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			Name = _message.Data;
			System.String[] words = Name.Split(new System.Char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
			System.Int32 temp = System.Convert.ToInt32(words[0]);

			Event _event = baseContext._Events.Where(p => p.ID == temp).FirstOrDefault();
			EventTime eventTime = baseContext._EventTimes.Where(p => p._event == _event).FirstOrDefault();
			_event.Work = true;

			baseContext.SaveChanges();

			await BotClient.EditMessageTextAsync
				(
				_message.From.Id,
				_message.Message.MessageId,
				"Название: " + _event.Name + "\nОписание: " + _event.Note + "\nМесто проведение: " + _event.Location + "\nВремя: " + eventTime.dateTime.TimeOfDay,
				replyMarkup: inlineButton.ChangeEvent
				);
		}

		public async void ChangeName(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			Event _event = baseContext._Events.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			_event.Name = _message.Text;

			await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nСодержание: " + _message.Text, replyMarkup: inlineButton.ChangeEvent);
			
			baseContext.SaveChanges();
		}

		public async void ChangeNote(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			Event _event = baseContext._Events.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			_event.Note = _message.Text;

			await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nСодержание: " + _message.Text, replyMarkup: inlineButton.ChangeEvent);

			baseContext.SaveChanges();
		}

		public void Dalete(CallbackQuery _message, DataBaseContext baseContext)
		{
			TempDate tempDate = baseContext._TempDate.Where(p => p.user.ID == _message.From.Id).FirstOrDefault();
			Event _event = baseContext._Events.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);

			foreach(EventTime eventTime in baseContext._EventTimes.Include("_event"))
			{
				if(eventTime.reminder != null)
				{
					if(eventTime.reminder.ID == _event.ID)
					{
						if(eventTime.dateTime.Date == tempDate.date.Date)
						{
							baseContext._EventTimes.Remove(eventTime);
						}
					}
				}
			}
			baseContext._Events.Remove(_event);
			baseContext.SaveChanges();
		}

		public async void ChangeTime(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			Event _event = baseContext._Events.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p._event.ID == _event.ID).FirstOrDefault();
			try
			{
				System.TimeSpan time = System.TimeSpan.Parse(_message.Text);
				System.TimeSpan timeSpan = eventTime.dateTime.TimeOfDay;
				eventTime.dateTime -= timeSpan;
				eventTime.dateTime = eventTime.dateTime.Date + time;

				await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nВремя изменено: " + _message.Text, replyMarkup: inlineButton.ChangeEvent);
			}
			catch
			{
				await BotClient.SendTextMessageAsync(_message.Chat.Id, "Время введено неверно: " + _message.Text, replyMarkup: inlineButton.ChangeEvent);
			}
			baseContext.SaveChanges();
		}

		public async void ChangeLocation(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext)
		{
			Event _event = baseContext._Events.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			_event.Location = _message.Text;

			
			await BotClient.SendTextMessageAsync(_message.Chat.Id, "Место проведения: " + _message.Text, replyMarkup: inlineButton.ChangeEvent);
		
			baseContext.SaveChanges();
		}
	}
}
