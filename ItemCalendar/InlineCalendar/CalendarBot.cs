using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Calendar.CalendarAlgorithm;
using Calendar.ItemCalendar;
using Calendar.Picture;
using Calendar.SQL;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.Command
{
	class CalendarBot : ICalendar
	{
		public System.String Name { get; } = CommandText.Calendar;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			Dictionary<System.Int32, Calendar> keyValues2 = keyValues as Dictionary<System.Int32, Calendar>;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 3000;
			baseContext.SaveChanges();
			Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup answer = inlineButtonCalendar.CalendarShow(keyValues2[_message.From.Id]);
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Выберете дату:",
					replyMarkup: answer
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("CalendarBot : ICalendar: " + ex);
			}
		}
	}

	class Next : ICalendar
	{
		public System.String Name { get; } = CommandText.Next;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			Dictionary<System.Int32, Calendar> keyValues2 = keyValues as Dictionary<System.Int32, Calendar>;
			CommandText.MyData.Clear();
			keyValues2[_message.From.Id].Next();
			Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup answer = inlineButtonCalendar.CalendarShow(keyValues2[_message.From.Id]);
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Выберете дату:",
					replyMarkup: answer
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Next : ICalendar: " + ex);
			}
		}
	}

	class Back : ICalendar
	{
		public System.String Name { get; } = CommandText.Back;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			Dictionary<System.Int32, Calendar> keyValues2 = keyValues as Dictionary<System.Int32, Calendar>;
			CommandText.MyData.Clear();
			keyValues2[_message.From.Id].Back();

			Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup answer = inlineButtonCalendar.CalendarShow(keyValues2[_message.From.Id]);
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Выберете дату:",
					replyMarkup: answer
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Back : ICalendar: " + ex);
			}
		}
	}

	class ChouseData
	{
		public System.String Name { get; set; }

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public async void Execute(TelegramBotClient botClient, System.Object message, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			System.Console.WriteLine(System.Convert.ToDateTime(_message.Data));
			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();

			TempDate tempDate = baseContext._TempDate.Where(p => p.user == users).FirstOrDefault();

			if(tempDate != null)
			{
				tempDate.date = System.Convert.ToDateTime(_message.Data);
				baseContext.SaveChanges();
			}
			else
			{
				baseContext._TempDate.Add(new TempDate() { user = users, date = System.Convert.ToDateTime(_message.Data) });
				baseContext.SaveChanges();
			}
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Ваша дата: " + _message.Data,
					replyMarkup: inlineButtonCalendar.ChouseDate
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ChouseData: " + ex);
			}

		}
	}

	class BackToMenuReminder : ICalendar
	{
		public System.String Name { get; } = CommandText.BackToMenuReminder;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			Dictionary<System.Int32, IEventCal> keyValues2 = keyValues as Dictionary<System.Int32, IEventCal>;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			System.Console.WriteLine(users.Work + " BackToMenuReminder");

			try
			{
				keyValues2[users.Work / 100].SetFalse(_message, baseContext);
			}
			catch
			{
				keyValues2[users.Work % 100].SetFalse(_message, baseContext);
			}
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Меню: ",
					replyMarkup: inlineButtonCalendar.ChouseDate
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("BackToMenuReminder : ICalendar: " + ex);
			}
		}
	}

	class AddTime : ICalendar
	{
		public System.String Name { get; } = CommandText.AddTime;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			System.Console.WriteLine("Time Add - " + users.Work);
			if((users.Work / 100) >= 26 && (users.Work / 100) <= 28)
			{
				users.Work = (users.Work / 100) + 3000;
			}
			else if((users.Work % 100) >= 26 && (users.Work % 100) <= 28)
			{
				users.Work = (users.Work % 100) + 3000;
			}
			else
			{
				System.Console.WriteLine(" AddTime does not work! ");
			}
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Ведите время(9:49): ",
					replyMarkup: inlineButtonCalendar.BackToCalendar
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("AddTime : ICalendar: " + ex);
			}
		}
	}

	class ShowOneDayCalendar : ICalendar
	{
		public System.String Name { get; set; } = CommandText.ShowOneDayCalendar;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			AlgorithmCalendar algorithmCalendar = new AlgorithmCalendar(_message);

			algorithmCalendar.mes?.Invoke(_message);

			System.Console.WriteLine(_message.Data);
			Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup an = inlineButtonCalendar.ShowCategoryCalendar(message, baseContext);
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Ваши события на заданную дату: ",
					replyMarkup: an
					);
			}
			catch
			{
				await botClient.SendTextMessageAsync
					(
					_message.From.Id,
					"Ваши события на заданную дату: ",
					replyMarkup: an
					);
			}
		}
	}

	class ShowItemCalndar
	{
		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			Dictionary<System.Int32, IEventCal> keyValues2 = keyValues as Dictionary<System.Int32, IEventCal>;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();

			keyValues2[users.Work % 100].ShowItem(botClient, message, inlineButtonCalendar, baseContext);

		}
	}

	#region Reminder
	class AddReminder : ICalendar
	{
		public System.String Name { get; } = CommandText.AddReminder;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 2626;
			baseContext.SaveChanges();

			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Напомнить мне... ",
					replyMarkup: inlineButtonCalendar.BackToCalendar
					);
			}
			catch
			{
				await botClient.SendTextMessageAsync
					(
					_message.From.Id,
					"Напомнить мне... ",
					replyMarkup: inlineButtonCalendar.BackToCalendar
					);
			}
		}
	}

	class EachDayReminder : ICalendar
	{
		public System.String Name { get; set; } = CommandText.EachDayReminder;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			Reminder reminder = baseContext._Reminders.Where(p => p.user.ID == users.ID).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p.reminder.ID == reminder.ID).FirstOrDefault();
			System.Console.WriteLine(" EachDay " + users.Work);
			eventTime.DoWork = 1;
			baseContext.SaveChanges();
			foreach(EventTime et in baseContext._EventTimes.Include("reminder"))
			{
				if(et.reminder != null)
				{
					if(reminder == et.reminder && et.DoWork != 1)
					{
						baseContext.Remove(et);
					}
				}
			}

			if(users.Work == 3426)
			{
				await botClient.EditMessageTextAsync(
						_message.From.Id,
						_message.Message.MessageId,
						"Уведомление установлены на каждый день.",
						replyMarkup: inlineButtonCalendar.ChangeReminder
						);
			}
			else
			{
				users.Work = 2600;
				try
				{
					await botClient.EditMessageTextAsync(
						_message.From.Id,
						_message.Message.MessageId,
						"Уведомление установлены на каждый день.",
						replyMarkup: inlineButtonCalendar.ChouseDate
						);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachDayReminder : ICalendar: " + ex);
				}
			}
			baseContext.SaveChanges();
		}
	}

	class EachWeakReminder : ICalendar
	{
		public System.String Name { get; set; } = CommandText.EachWeakReminder;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			Reminder reminder = baseContext._Reminders.Where(p => p.user.ID == users.ID).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p.reminder.ID == reminder.ID).FirstOrDefault();
			System.Console.WriteLine(" EachDay " + users.Work);
			eventTime.DoWork = 2;

			baseContext.SaveChanges();
			foreach(EventTime et in baseContext._EventTimes.Include("reminder"))
			{
				if(et.reminder != null)
				{
					if(reminder == et.reminder && et.DoWork != 2)
					{
						baseContext.Remove(et);
					}
				}
			}

			if(users.Work == 3426)
			{
				try
				{
					await botClient.EditMessageTextAsync
							(
							_message.From.Id,
							_message.Message.MessageId,
							"Уведомление установлены на каждую неделю.",
							replyMarkup: inlineButtonCalendar.ChangeReminder
							);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachWeakReminder : ICalendar: " + ex);
				}
			}
			else
			{
				users.Work = 2600;
				try
				{
					await botClient.EditMessageTextAsync
						(
						_message.From.Id,
						_message.Message.MessageId,
						"Уведомление установлены на каждую неделю.",
						replyMarkup: inlineButtonCalendar.ChouseDate
						);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachWeakReminder : ICalendar: " + ex);
				}
			}
			baseContext.SaveChanges();
		}
	}

	class EachMouthReminder : ICalendar
	{
		public System.String Name { get; set; } = CommandText.EachMouthReminder;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			Reminder reminder = baseContext._Reminders.Where(p => p.user.ID == users.ID).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p.reminder.ID == reminder.ID).FirstOrDefault();
			System.Console.WriteLine(" EachDay " + users.Work);
			eventTime.DoWork = 3;

			baseContext.SaveChanges();
			foreach(EventTime et in baseContext._EventTimes.Include("reminder"))
			{
				if(et.reminder != null)
				{
					if(reminder == et.reminder && et.DoWork != 3)
					{
						baseContext.Remove(et);
					}
				}
			}

			if(users.Work == 3426)
			{
				try
				{
					await botClient.EditMessageTextAsync(
							_message.From.Id,
							_message.Message.MessageId,
							"Уведомление установлены на каждый месяц.",
							replyMarkup: inlineButtonCalendar.ChangeReminder
							);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachMouthReminder : ICalendar: " + ex);
				}
			}
			else
			{
				users.Work = 2600;
				try
				{
					await botClient.EditMessageTextAsync(
						_message.From.Id,
						_message.Message.MessageId,
						"Уведомление установлены на каждый месяц.",
						replyMarkup: inlineButtonCalendar.ChouseDate
						);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachMouthReminder : ICalendar: " + ex);
				}
			}
			baseContext.SaveChanges();
		}
	}

	class EachYearReminder : ICalendar
	{
		public System.String Name { get; set; } = CommandText.EachYearReminder;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			Reminder reminder = baseContext._Reminders.Where(p => p.user.ID == users.ID).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p.reminder.ID == reminder.ID).FirstOrDefault();
			System.Console.WriteLine(" EachDay " + users.Work);
			eventTime.DoWork = 4;

			baseContext.SaveChanges();
			foreach(EventTime et in baseContext._EventTimes.Include("reminder"))
			{
				if(et.reminder != null)
				{
					if(reminder == et.reminder && et.DoWork != 4)
					{
						baseContext.Remove(et);
					}
				}
			}

			if(users.Work == 3426)
			{
				try
				{
					await botClient.EditMessageTextAsync(
							_message.From.Id,
							_message.Message.MessageId,
							"Уведомление установлены на каждый год.",
							replyMarkup: inlineButtonCalendar.ChangeReminder
							);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachYearReminder : ICalendar: " + ex);
				}
			}
			else
			{
				users.Work = 2600;
				try
				{
					await botClient.EditMessageTextAsync(
						_message.From.Id,
						_message.Message.MessageId,
						"Уведомление установлены на каждый год.",
						replyMarkup: inlineButtonCalendar.ChouseDate
						);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachYearReminder : ICalendar: " + ex);
				}
			}
			baseContext.SaveChanges();
		}
	}

	class AddReminderReiteration : ICalendar
	{
		public System.String Name { get; set; } = CommandText.AddReminderReiteration;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Повторение: ",
					replyMarkup: inlineButtonCalendar.ReminderShow
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("AddReminderReiteration : ICalendar: " + ex);
			}
		}
	}

	class AddContextReminder : ICalendar
	{
		public System.String Name { get; } = CommandText.AddContextReminder;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 2926;
			baseContext.SaveChanges();
			//users.Work = (users.Work / 100) + 2900;
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите описание: ",
					replyMarkup: inlineButtonCalendar.BackToReminder
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("AddContextReminder : ICalendar: " + ex);
			}
		}
	}

	class ChangeReminderName : ICalendar
	{
		public System.String Name { get; } = CommandText.ChangeReminderName;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 3326;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите новое название: ",
					replyMarkup: inlineButtonCalendar.BackToChangeReminder
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ChangeReminderName : ICalendar: " + ex);
			}
		}
	}

	class ChangeReminderNote : ICalendar
	{
		public System.String Name { get; } = CommandText.ChangeReminderNote;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 3426;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите новое название: ",
					replyMarkup: inlineButtonCalendar.BackToChangeReminder
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ChangeReminderNote : ICalendar: " + ex);
			}
		}
	}

	class ChangeReminderReiteration : ICalendar
	{
		public System.String Name { get; } = CommandText.ChangeReminderReiteration;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 3426;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите новое название: ",
					replyMarkup: inlineButtonCalendar.ReminderShow
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ChangeReminderReiteration : ICalendar: " + ex);
			}
		}
	}

	class ShowReminder : ICalendar
	{
		public System.String Name { get; } = CommandText.ShowReminder;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 2626;
			baseContext.SaveChanges();

			Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup answer = inlineButtonCalendar.ShowReminder(message, baseContext);
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Напоминание: ",
					replyMarkup: answer
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ShowReminder : ICalendar: " + ex);
			}
		}
	}

	class BackToReminder : ICalendar
	{
		public System.String Name { get; } = CommandText.BackToReminder;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			TempDate tempDate = baseContext._TempDate.Where(p => p.user.ID == users.ID).FirstOrDefault();
			Calendar calendar = new Calendar();

			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					tempDate.date.Day + " " + calendar.NameCalendar(tempDate.date) + ": ",
					replyMarkup: inlineButtonCalendar.AddReminder
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("BackToReminder : ICalendar: " + ex);
			}
		}
	}

	class BackToChangeReminder : ICalendar
	{
		public System.String Name { get; } = CommandText.BackToChangeReminder;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			TempDate tempDate = baseContext._TempDate.Where(p => p.user.ID == users.ID).FirstOrDefault();
			Reminder reminder = baseContext._Reminders.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			Calendar calendar = new Calendar();

			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					tempDate.date.Day + " " + calendar.NameCalendar(tempDate.date) + ": "
					+ "\nНазвание: " + reminder.Name + "\nОписание: " + reminder.Note,
					replyMarkup: inlineButtonCalendar.BackToChangeReminder
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("BackToChangeReminder : ICalendar: " + ex);
			}
		}
	}
	#endregion

	#region Purpose
	class AddPurpose : ICalendar
	{
		public System.String Name { get; } = CommandText.AddPurpose;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 2727;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Цель: ",
					replyMarkup: inlineButtonCalendar.BackToCalendar
					);
			}
			catch
			{
				await botClient.SendTextMessageAsync
					(
					_message.From.Id,
					"Цель: ",
					replyMarkup: inlineButtonCalendar.BackToCalendar
					);
			}
		}
	}

	class EachDayPurpose : ICalendar
	{
		public System.String Name { get; set; } = CommandText.EachDayPurpose;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			Purpose reminder = baseContext._Purposes.Where(p => p.user.ID == users.ID).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p.purpose.ID == reminder.ID).FirstOrDefault();
			System.Console.WriteLine(" EachDay " + users.Work);
			eventTime.DoWork = 1;
			baseContext.SaveChanges();
			foreach(EventTime et in baseContext._EventTimes.Include("purpose"))
			{
				if(et.reminder != null)
				{
					if(reminder == et.purpose && et.DoWork != 1)
					{
						baseContext.Remove(et);
					}
				}
			}
			if(users.Work == 3427)
			{
				try
				{
					await botClient.EditMessageTextAsync(
							_message.From.Id,
							_message.Message.MessageId,
							"Уведомление установление на каждый день.",
							replyMarkup: inlineButtonCalendar.ChangePurpose
							);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachDayPurpose : ICalendar: " + ex);
				}
			}
			else
			{
				users.Work = 2700;
				try
				{
					await botClient.EditMessageTextAsync(
						_message.From.Id,
						_message.Message.MessageId,
						"Уведомление установление на каждый день.",
						replyMarkup: inlineButtonCalendar.AddPurpose
						);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachDayPurpose : ICalendar: " + ex);
				}
			}
			baseContext.SaveChanges();
		}
	}

	class EachWeakPurpose : ICalendar
	{
		public System.String Name { get; set; } = CommandText.EachWeakPurpose;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			Purpose reminder = baseContext._Purposes.Where(p => p.user.ID == users.ID).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p.purpose.ID == reminder.ID).FirstOrDefault();
			System.Console.WriteLine(" EachDay " + users.Work);
			eventTime.DoWork = 2;

			baseContext.SaveChanges();
			foreach(EventTime et in baseContext._EventTimes.Include("purpose"))
			{
				if(et.reminder != null)
				{
					if(reminder == et.purpose && et.DoWork != 2)
					{
						baseContext.Remove(et);
					}
				}
			}

			if(users.Work == 3427)
			{
				try
				{
					await botClient.EditMessageTextAsync(
							_message.From.Id,
							_message.Message.MessageId,
							"Уведомление установлены на каждую неделю.",
							replyMarkup: inlineButtonCalendar.ChangePurpose
							);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachWeakPurpose : ICalendar: " + ex);
				}
			}
			else
			{
				users.Work = 2700;
				try
				{
					await botClient.EditMessageTextAsync(
						_message.From.Id,
						_message.Message.MessageId,
						"Уведомление установлены на каждую неделю.",
						replyMarkup: inlineButtonCalendar.AddPurpose
						);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachWeakPurpose : ICalendar: " + ex);
				}
			}
			baseContext.SaveChanges();
		}
	}

	class EachMouthPurpose : ICalendar
	{
		public System.String Name { get; set; } = CommandText.EachMouthPurpose;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			Purpose reminder = baseContext._Purposes.Where(p => p.user.ID == users.ID).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p.purpose.ID == reminder.ID).FirstOrDefault();
			System.Console.WriteLine(" EachDay " + users.Work);
			eventTime.DoWork = 3;

			baseContext.SaveChanges();
			foreach(EventTime et in baseContext._EventTimes.Include("purpose"))
			{
				if(et.reminder != null)
				{
					if(reminder == et.purpose && et.DoWork != 3)
					{
						baseContext.Remove(et);
					}
				}
			}

			if(users.Work == 3427)
			{
				try
				{
					await botClient.EditMessageTextAsync(
							_message.From.Id,
							_message.Message.MessageId,
							"Уведомление установлены на каждый месяц.",
							replyMarkup: inlineButtonCalendar.ChangePurpose
							);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachMouthPurpose : ICalendar: " + ex);
				}
			}
			else
			{
				users.Work = 2700;
				try
				{
					await botClient.EditMessageTextAsync(
						_message.From.Id,
						_message.Message.MessageId,
						"Уведомление установлены на каждый месяц.",
						replyMarkup: inlineButtonCalendar.AddPurpose
						);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachMouthPurpose : ICalendar: " + ex);
				}
			}
			baseContext.SaveChanges();
		}
	}

	class EachYearPurpose : ICalendar
	{
		public System.String Name { get; set; } = CommandText.EachYearPurpose;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			Purpose reminder = baseContext._Purposes.Where(p => p.user.ID == users.ID).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p.purpose.ID == reminder.ID).FirstOrDefault();
			System.Console.WriteLine(" EachDay " + users.Work);
			eventTime.DoWork = 4;

			baseContext.SaveChanges();
			foreach(EventTime et in baseContext._EventTimes.Include("purpose"))
			{
				if(et.reminder != null)
				{
					if(reminder == et.purpose && et.DoWork != 4)
					{
						baseContext.Remove(et);
					}
				}
			}

			if(users.Work == 3427)
			{
				try
				{
					await botClient.EditMessageTextAsync(
							_message.From.Id,
							_message.Message.MessageId,
							"Уведомление установлены на каждый год.",
							replyMarkup: inlineButtonCalendar.ChangePurpose
							);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachYearPurpose : ICalendar: " + ex);
				}
			}
			else
			{
				users.Work = 2700;
				try
				{
					await botClient.EditMessageTextAsync(
						_message.From.Id,
						_message.Message.MessageId,
						"Уведомление установлены на каждый год.",
						replyMarkup: inlineButtonCalendar.AddPurpose
						);
				}
				catch
				{
					System.Console.WriteLine("CalendarBot - EachYearPurpose");
				}
				baseContext.SaveChanges();
			}
		}
	}

	class AddPurooseReiteration : ICalendar
	{
		public System.String Name { get; set; } = CommandText.AddPurooseReiteration;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Повторение: ",
					replyMarkup: inlineButtonCalendar.PurposeShow
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("AddPurooseReiteration : ICalendar: " + ex);
			}
		}
	}

	class AddContextPurpose : ICalendar
	{
		public System.String Name { get; } = CommandText.AddContextPurpose;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 2927;
			baseContext.SaveChanges();
			//users.Work = (users.Work / 100) + 2900;
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите описание: ",
					replyMarkup: inlineButtonCalendar.BackToPurpose
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("AddContextPurpose : ICalendar: " + ex);
			}
		}
	}

	class DurationPurpose : ICalendar
	{
		public System.String Name { get; } = CommandText.DurationPurpose;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Продолжительность: ",
					replyMarkup: inlineButtonCalendar.DurationPurpose
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("DurationPurpose : ICalendar: " + ex);
			}
		}
	}

	class DurationPurposeTime15 : ICalendar
	{
		public System.String Name { get; } = CommandText.DurationPurposeTime15;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			Purpose purpose = baseContext._Purposes.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);


			purpose.Duration = System.TimeSpan.Parse("0:15");

			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Продолжительность добавлена. ",
					replyMarkup: inlineButtonCalendar.AddPurpose
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("DurationPurposeTime15 : ICalendar: " + ex);
			}
		}
	}

	class DurationPurposeTime30 : ICalendar
	{
		public System.String Name { get; } = CommandText.DurationPurposeTime30;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			Purpose purpose = baseContext._Purposes.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);


			purpose.Duration = System.TimeSpan.Parse("0:30");

			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Продолжительность добавлена. ",
					replyMarkup: inlineButtonCalendar.AddPurpose
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("DurationPurposeTime30 : ICalendar: " + ex);
			}
		}
	}

	class DurationPurposeTime1 : ICalendar
	{
		public System.String Name { get; } = CommandText.DurationPurposeTime1;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			Purpose purpose = baseContext._Purposes.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);


			purpose.Duration = System.TimeSpan.Parse("1:0");

			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Продолжительность добавлена. ",
					replyMarkup: inlineButtonCalendar.AddPurpose
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("DurationPurposeTime1 : ICalendar: " + ex);
			}
		}
	}

	class DurationPurposeTime2 : ICalendar
	{
		public System.String Name { get; } = CommandText.DurationPurposeTime2;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			Purpose purpose = baseContext._Purposes.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);


			purpose.Duration = System.TimeSpan.Parse("2:0");

			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Продолжительность добавлена. ",
					replyMarkup: inlineButtonCalendar.AddPurpose
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("DurationPurposeTime2 : ICalendar: " + ex);
			}
		}
	}

	class ChangePurposeName : ICalendar
	{
		public System.String Name { get; } = CommandText.ChangePurposeName;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 3327;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите новое название: ",
					replyMarkup: inlineButtonCalendar.BackToChangePurpose
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ChangePurposeName : ICalendar: " + ex);
			}
		}
	}

	class ChangePurposeNote : ICalendar
	{
		public System.String Name { get; } = CommandText.ChangePurposeNote;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 3427;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите новое название: ",
					replyMarkup: inlineButtonCalendar.BackToChangePurpose
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ChangePurposeNote : ICalendar: " + ex);
			}
		}
	}

	class ChangePurposeReiteration : ICalendar
	{
		public System.String Name { get; } = CommandText.ChangePurposeReiteration;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 3427;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите новое название: ",
					replyMarkup: inlineButtonCalendar.PurposeShow
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ChangePurposeReiteration : ICalendar: " + ex);
			}
		}
	}

	class ShowPurpose : ICalendar
	{
		public System.String Name { get; } = CommandText.ShowPurpose;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 2727;
			baseContext.SaveChanges();

			Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup answer = inlineButtonCalendar.ShowPurpose(message, baseContext);
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Цель: ",
					replyMarkup: answer
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ShowPurpose : ICalendar: " + ex);
			}
		}
	}

	class BackToPurpose : ICalendar
	{
		public System.String Name { get; } = CommandText.BackToPurpose;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			TempDate tempDate = baseContext._TempDate.Where(p => p.user.ID == users.ID).FirstOrDefault();
			Calendar calendar = new Calendar();

			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					tempDate.date.Day + " " + calendar.NameCalendar(tempDate.date) + ": ",
					replyMarkup: inlineButtonCalendar.AddPurpose
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("BackToPurpose : ICalendar: " + ex);
			}
		}
	}

	class BackToChangePurpose : ICalendar
	{
		public System.String Name { get; } = CommandText.BackToChangePurpose;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			TempDate tempDate = baseContext._TempDate.Where(p => p.user.ID == users.ID).FirstOrDefault();
			Purpose purpose = baseContext._Purposes.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			Calendar calendar = new Calendar();

			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					tempDate.date.Day + " " + calendar.NameCalendar(tempDate.date) + ": "
					+ "\nЦель: " + purpose.Name + "\nОписание: " + purpose.Note + "\nПродолжительность: "
					+ purpose.Duration.Hours.ToString() + ":" + purpose.Duration.Minutes.ToString(),
					replyMarkup: inlineButtonCalendar.ChangePurpose
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("BackToChangePurpose : ICalendar: " + ex);
			}
		}
	}
	#endregion

	#region Event
	class AddEventNotBusy : ICalendar
	{
		public System.String Name { get; } = CommandText.AddEventNotBusy;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 2828;

			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Мероприятие: ",
					replyMarkup: inlineButtonCalendar.BackToCalendar
					);
			}
			catch
			{
				await botClient.SendTextMessageAsync
					(
					_message.From.Id,
					"Мероприятие: ",
					replyMarkup: inlineButtonCalendar.BackToCalendar
					);
			}
		}
	}

	class AddEventBusy : ICalendar
	{
		public System.String Name { get; } = CommandText.AddEventBusy;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 2828;

			Event _event = baseContext._Events.Where(p => p.user == users).FirstOrDefault(p => p.Work == true);
			_event.Busy = false;

			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Мероприятие: ",
					replyMarkup: inlineButtonCalendar.AddEventBusy
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("AddEventBusy : ICalendar: " + ex);
			}
		}
	}

	class AddEventNotBusy2 : ICalendar
	{
		public System.String Name { get; } = CommandText.AddEventNotBusy2;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 2828;

			Event _event = baseContext._Events.Where(p => p.user == users).FirstOrDefault(p => p.Work == true);
			_event.Busy = true;

			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Мероприятие: ",
					replyMarkup: inlineButtonCalendar.AddEventNotBusy
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("AddEventNotBusy2 : ICalendar: " + ex);
			}
		}
	}

	class EachDayEvent : ICalendar
	{
		public System.String Name { get; set; } = CommandText.EachDayEvent;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			Event _event = baseContext._Events.Where(p => p.user.ID == users.ID).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p._event.ID == _event.ID).FirstOrDefault();
			System.Console.WriteLine(" EachDay " + users.Work);
			eventTime.DoWork = 1;

			baseContext.SaveChanges();
			foreach(EventTime et in baseContext._EventTimes.Include("_event"))
			{
				if(et.reminder != null)
				{
					if(_event == et._event && et.DoWork != 1)
					{
						baseContext.Remove(et);
					}
				}
			}

			if(users.Work == 3428)
			{
				try
				{
					await botClient.EditMessageTextAsync(
							_message.From.Id,
							_message.Message.MessageId,
							"Уведомление установление на каждый день.",
							replyMarkup: inlineButtonCalendar.ChangeEvent
							);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachDayEvent : ICalendar: " + ex);
				}
			}
			else
			{
				users.Work = 2800;
				try
				{
					if(_event.Busy == true)
					{
						await botClient.EditMessageTextAsync(_message.From.Id,
						_message.Message.MessageId,
						"Уведомление установление на каждый день.",
						replyMarkup: inlineButtonCalendar.AddEventNotBusy);
					}
					else
					{
						await botClient.EditMessageTextAsync(
							_message.From.Id,
							_message.Message.MessageId,
							"Уведомление установление на каждый день.",
							replyMarkup: inlineButtonCalendar.AddEventBusy);
					}
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachDayEvent : ICalendar: " + ex);
				}
			}
			baseContext.SaveChanges();

		}
	}

	class EachWeakEvent : ICalendar
	{
		public System.String Name { get; set; } = CommandText.EachWeakEvent;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			Event _event = baseContext._Events.Where(p => p.user.ID == users.ID).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p._event.ID == _event.ID).FirstOrDefault();
			System.Console.WriteLine(" EachDay " + users.Work);
			eventTime.DoWork = 2;

			baseContext.SaveChanges();
			foreach(EventTime et in baseContext._EventTimes.Include("_event"))
			{
				if(et.reminder != null)
				{
					if(_event == et._event && et.DoWork != 2)
					{
						baseContext.Remove(et);
					}
				}
			}

			if(users.Work == 3428)
			{
				try
				{
					await botClient.EditMessageTextAsync(
							_message.From.Id,
							_message.Message.MessageId,
							"Уведомление установлены на каждую неделю.",
							replyMarkup: inlineButtonCalendar.ChangeEvent
							);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachWeakEvent : ICalendar: " + ex);
				}
			}
			else
			{
				users.Work = 2800;
				try
				{
					if(_event.Busy == true)
					{
						await botClient.EditMessageTextAsync(_message.From.Id,
						_message.Message.MessageId,
						"Уведомление установлены на каждую неделю.",
						replyMarkup: inlineButtonCalendar.AddEventNotBusy);
					}
					else
					{
						await botClient.EditMessageTextAsync(
							_message.From.Id,
							_message.Message.MessageId,
							"Уведомление установлены на каждую неделю.",
							replyMarkup: inlineButtonCalendar.AddEventBusy);
					}
				}
				catch
				{
					System.Console.WriteLine("CalendarBot - EachWeakReiteration");
				}
			}
			baseContext.SaveChanges();
		}
	}

	class EachMouthEvent : ICalendar
	{
		public System.String Name { get; set; } = CommandText.EachMouthEvent;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			Event _event = baseContext._Events.Where(p => p.user.ID == users.ID).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p._event.ID == _event.ID).FirstOrDefault();
			System.Console.WriteLine(" EachDay " + users.Work);
			eventTime.DoWork = 3;

			baseContext.SaveChanges();
			foreach(EventTime et in baseContext._EventTimes.Include("_event"))
			{
				if(et.reminder != null)
				{
					if(_event == et._event && et.DoWork != 3)
					{
						baseContext.Remove(et);
					}
				}
			}

			if(users.Work == 3428)
			{
				try
				{
					await botClient.EditMessageTextAsync(
							_message.From.Id,
							_message.Message.MessageId,
							"Уведомление установлены на каждый месяц.",
							replyMarkup: inlineButtonCalendar.ChangeEvent
							);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachMouthEvent : ICalendar: " + ex);
				}
			}
			else
			{
				users.Work = 2800;
				try
				{
					if(_event.Busy == true)
					{
						await botClient.EditMessageTextAsync(_message.From.Id,
						_message.Message.MessageId,
						"Уведомление установлены на каждый месяц.",
						replyMarkup: inlineButtonCalendar.AddEventNotBusy);
					}
					else
					{
						await botClient.EditMessageTextAsync(
							_message.From.Id,
							_message.Message.MessageId,
							"Уведомление установлены на каждый месяц.",
							replyMarkup: inlineButtonCalendar.AddEventBusy);
					}
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachMouthEvent : ICalendar: " + ex);
				}
			}

			baseContext.SaveChanges();
		}
	}

	class EachYearEvent : ICalendar
	{
		public System.String Name { get; set; } = CommandText.EachYearEvent;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			Event _event = baseContext._Events.Where(p => p.user.ID == users.ID).FirstOrDefault(p => p.Work == true);
			EventTime eventTime = baseContext._EventTimes.Where(p => p._event.ID == _event.ID).FirstOrDefault();
			System.Console.WriteLine(" EachDay " + users.Work);
			eventTime.DoWork = 4;

			baseContext.SaveChanges();
			foreach(EventTime et in baseContext._EventTimes.Include("_event"))
			{
				if(et.reminder != null)
				{
					if(_event == et._event && et.DoWork != 4)
					{
						baseContext.Remove(et);
					}
				}
			}

			if(users.Work == 3428)
			{
				try
				{
					await botClient.EditMessageTextAsync(
							_message.From.Id,
							_message.Message.MessageId,
							"Уведомление установлены на каждый год.",
							replyMarkup: inlineButtonCalendar.ChangeEvent
							);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("EachYearEvent : ICalendar: " + ex);
				}
			}
			else
			{
				users.Work = 2800;
				try
				{
					if(_event.Busy == true)
					{
						await botClient.EditMessageTextAsync(_message.From.Id,
						_message.Message.MessageId,
						"Уведомление установлены на каждый год.",
						replyMarkup: inlineButtonCalendar.AddEventNotBusy);
					}
					else
					{
						await botClient.EditMessageTextAsync(
							_message.From.Id,
							_message.Message.MessageId,
							"Уведомление установлены на каждый год.",
							replyMarkup: inlineButtonCalendar.AddEventBusy);
					}
				}
				catch
				{
					System.Console.WriteLine("CalendarBot - EachYearReiteration");
				}
			}
			baseContext.SaveChanges();
		}
	}

	class AddEventReiteration : ICalendar
	{
		public System.String Name { get; set; } = CommandText.AddEventReiteration;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Повторение: ",
					replyMarkup: inlineButtonCalendar.EventShow
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("AddEventReiteration : ICalendar: " + ex);
			}
		}
	}

	class AddContextEvent : ICalendar
	{
		public System.String Name { get; } = CommandText.AddContextEvent;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 2928;
			baseContext.SaveChanges();
			//users.Work = (users.Work / 100) + 2900;
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите описание: ",
					replyMarkup: inlineButtonCalendar.BackToEvent
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("AddContextEvent : ICalendar: " + ex);
			}
		}
	}

	class AddLocation : ICalendar
	{
		public System.String Name { get; } = CommandText.AddLocation;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User user = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			user.Work = 3228;
			baseContext.SaveChanges();

			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите место проведения: ",
					replyMarkup: inlineButtonCalendar.BackToEvent
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("AddLocation : ICalendar: " + ex);
			}
		}
	}

	class ChangeEventName : ICalendar
	{
		public System.String Name { get; } = CommandText.ChangeEventName;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 3328;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите новое название: ",
					replyMarkup: inlineButtonCalendar.BackToChangeEvent
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ChangeEventName : ICalendar: " + ex);
			}
		}
	}

	class ChangeEventNote : ICalendar
	{
		public System.String Name { get; } = CommandText.ChangeEventNote;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 3428;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите новое название: ",
					replyMarkup: inlineButtonCalendar.BackToChangeEvent
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ChangeEventNote : ICalendar: " + ex);
			}
		}
	}

	class ChangeEventReiteration : ICalendar
	{
		public System.String Name { get; } = CommandText.ChangeEventReiteration;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 3428;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите новое название: ",
					replyMarkup: inlineButtonCalendar.EventShow
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ChangeEventReiteration : ICalendar: " + ex);
			}
		}
	}

	class ChangeEventLocation : ICalendar
	{
		public System.String Name { get; } = CommandText.ChangeEventLocation;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 3628;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите новое место проведение: ",
					replyMarkup: inlineButtonCalendar.BackToChangeEvent
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ChangeEventLocation : ICalendar: " + ex);
			}
		}
	}

	class ShowEvent : ICalendar
	{
		public System.String Name { get; } = CommandText.ShowEvent;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 2828;
			baseContext.SaveChanges();

			Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup answer = inlineButtonCalendar.ShowEvent(message, baseContext);

			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Мероприятие: ",
					replyMarkup: answer
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ShowEvent : ICalendar: " + ex);
			}
		}
	}

	class BackToevent : ICalendar
	{
		public System.String Name { get; } = CommandText.BackToEvent;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			TempDate tempDate = baseContext._TempDate.Where(p => p.user.ID == users.ID).FirstOrDefault();
			Calendar calendar = new Calendar();
			Event _event = baseContext._Events.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			try
			{
				if(_event.Busy == true)
				{
					await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					tempDate.date.Day + " " + calendar.NameCalendar(tempDate.date) + ": ",
					replyMarkup: inlineButtonCalendar.AddEventNotBusy
					);
				}
				else
				{
					await botClient.EditMessageTextAsync(
					   _message.From.Id,
					   _message.Message.MessageId,
					   tempDate.date.Day + " " + calendar.NameCalendar(tempDate.date) + ": ",
					   replyMarkup: inlineButtonCalendar.AddEventBusy
					   );
				}
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("BackToevent : ICalendar: " + ex);
			}
		}
	}

	class BackToChangeEvent : ICalendar
	{
		public System.String Name { get; } = CommandText.BackToChangeEvent;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			TempDate tempDate = baseContext._TempDate.Where(p => p.user.ID == users.ID).FirstOrDefault();
			Calendar calendar = new Calendar();
			Event _event = baseContext._Events.Where(p => p.user.ID == _message.From.Id).FirstOrDefault(p => p.Work == true);
			try
			{
				if(_event.Busy == true)
				{
					await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					tempDate.date.Day + " " + calendar.NameCalendar(tempDate.date) + ": "
					+ "\nНазвание: " + _event.Name + "\nОписание: " + _event.Note + "\nМесто проведение: " + _event.Location,
					replyMarkup: inlineButtonCalendar.AddEventNotBusy
					);
				}
				else
				{
					await botClient.EditMessageTextAsync(
					   _message.From.Id,
					   _message.Message.MessageId,
					   tempDate.date.Day + " " + calendar.NameCalendar(tempDate.date) + ": "
					   + "\nНазвание: " + _event.Name + "\nОписание: " + _event.Note + "\nМесто проведение: " + _event.Location,
					   replyMarkup: inlineButtonCalendar.AddEventBusy
					   );
				}
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("BackToChangeEvent : ICalendar: " + ex);
			}
		}
	}
	#endregion

	class ChangeAllTime : ICalendar
	{
		public System.String Name { get; } = CommandText.ChangeAllTime;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			Dictionary<System.Int32, IEventCal> keyValues2 = keyValues as Dictionary<System.Int32, IEventCal>;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = (users.Work % 100) + 3500;
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите новое название: ",
					replyMarkup: inlineButtonCalendar.BackToCalendar
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ChangeAllTime : ICalendar: " + ex);
			}
			baseContext.SaveChanges();
		}
	}

	class DeleteAllEvent : ICalendar
	{
		public System.String Name { get; } = CommandText.DeleteAllEvent;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		DataBaseContext BaseContext { get; set; } = new DataBaseContext();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			Dictionary<System.Int32, IEventCal> keyValues2 = keyValues as Dictionary<System.Int32, IEventCal>;

			User users = BaseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			try
			{
				keyValues2[users.Work / 100].Dalete(_message, baseContext);
			}
			catch
			{
				keyValues2[users.Work % 100].Dalete(_message, baseContext);
			}

			users.Work = 3000;
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Введите новое название: ",
					replyMarkup: inlineButtonCalendar.ChouseDate
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("DeleteAllEvent : ICalendar: " + ex);
			}
			BaseContext.SaveChanges();
		}
	}

	class BackToChoseDate : ICalendar
	{
		public System.String Name { get; } = CommandText.BackToChoseDate;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			Dictionary<System.Int32, IEventCal> keyValues2 = keyValues as Dictionary<System.Int32, IEventCal>;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			try
			{
				keyValues2[users.Work / 100].SetFalse(_message, baseContext);
			}
			catch
			{
				keyValues2[users.Work % 100].SetFalse(_message, baseContext);
			}
			baseContext.SaveChanges();


			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Меню:",
					replyMarkup: inlineButtonCalendar.ChouseDate
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("BackToChoseDate : ICalendar: " + ex);
			}
		}
	}

	class BackToCalendar : ICalendar
	{
		public System.String Name { get; } = CommandText.BackToCalendar;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			Dictionary<System.Int32, Calendar> keyValues2 = keyValues as Dictionary<System.Int32, Calendar>;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();

			users.Work = 3000;
			baseContext.SaveChanges();

			Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup answer = inlineButtonCalendar.CalendarShow(keyValues2[_message.From.Id]);
			try
			{
				await botClient.EditMessageTextAsync(
					_message.From.Id,
					_message.Message.MessageId,
					"Выберете дату:",
					replyMarkup: answer
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("BackToCalendar : ICalendar: " + ex);
			}
		}
	}

	class OrganizerSattisticDay : ICalendar
	{
		public System.String Name { get; } = CommandText.OrganizerSattisticDay;

		readonly InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			Statistics statistics = baseContext._Statistics.Where(p => p.user == users).FirstOrDefault(p => p.Day == System.DateTime.Today);
			TempDate date = baseContext._TempDate.Where(p => p.user == users).FirstOrDefault();
			if(statistics == null)
			{
				baseContext._Statistics.Add(new Statistics()
				{
					user = users,
					Day = System.DateTime.Today,
					TimeInDayToRelax = new System.TimeSpan(0, 0, 0),
					TimeInDayToWork = new System.TimeSpan(0, 0, 0)
				});
			}
			baseContext.SaveChanges();

			PictureB picture = new PictureB();
			if(picture.SetPicture(users, _message, statistics, date, baseContext))
			{

				System.String str = @"E:\Repo C#\Calendar\Calendar\" + _message.From.Id + ".jpeg";

				Bitmap bmp;
				using(MemoryStream ms = new MemoryStream(statistics.image))
				{
					bmp = new Bitmap(ms);

					bmp.Save(str, System.Drawing.Imaging.ImageFormat.Jpeg);

				}

				FileStream stream = new FileStream(str, FileMode.Open);
				try
				{
					await botClient.SendPhotoAsync(_message.From.Id, stream, replyMarkup: inlineButtonCalendar.ChouseDate);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("OrganizerSattisticDay : ICalendar: " + ex);
				}
				stream.Close();

				if(System.IO.File.Exists(str))
				{
					// If file found, delete it    
					System.IO.File.Delete(str);
				}
			}
			else
			{
				try
				{
					await botClient.EditMessageTextAsync(
						_message.From.Id,
						_message.Message.MessageId,
						"Нет статистики за эту дату:",
						replyMarkup: inlineButtonCalendar.ChouseDate
						);
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine("OrganizerSattisticDay : ICalendar: " + ex);
				}
			}
		}
	}
}