using System.Collections.Generic;
using System.Linq;
using Calendar.ItemCategory;
using Calendar.OrganizerTelegram;
using Calendar.Settings;
using Calendar.SQL;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace Calendar
{
	class StartMenu : ICommand
	{
		public System.String Name { get; } = CommandText.Start;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			Message _message = message as Message;

			if(!baseContext._User.Any(p => p.ID == _message.From.Id))
			{
				baseContext._User.Add(new User()
				{
					ID = _message.From.Id,
					Work = 0,
					SendMessageTelegram = true,
					SendMessageE_Mail = false,
					TimeToRelaxation = System.TimeSpan.FromMinutes(10),
					TimeToWork = System.TimeSpan.FromHours(1),
					TimeWork = false
				});
			}

			baseContext.SaveChanges();
			try
			{
				await botClient.SendTextMessageAsync(_message.Chat.Id, "Привет друг мой, " + _message.From.FirstName, replyMarkup: inlineButton.Start);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Exception: StartMenu : ICommand - " + ex);
			}

		}
	}

	class BackToStart : ICalendar
	{
		public System.String Name { get; } = CommandText.BackToStart;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			Dictionary<System.Int32, Calendar> keyValues2 = keyValues as Dictionary<System.Int32, Calendar>;

			CommandText.MyData.Clear();
			keyValues2.Remove(_message.From.Id);
			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			if(users != null)
			{
				TempDate tempDate = baseContext._TempDate.Where(p => p.user == users).FirstOrDefault();


				users.Work = 0;
				baseContext.SaveChanges();
				try
				{
					baseContext._TempDate.Remove(tempDate);
				}
				catch
				{
					System.Console.WriteLine("baseContext._TempDate.Remove(tempDate);");
				}
				baseContext.SaveChanges();
			}
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Привет друг мой, " + _message.From.FirstName, replyMarkup: inlineButton.Start);
			}
			catch
			{
				await botClient.SendTextMessageAsync
					(
					_message.From.Id,
					"Привет друг мой, " + _message.From.FirstName,
					replyMarkup: inlineButton.Start
					);
			}
		}
	}

	class StartSettingsOne : CheckSettings, ICommand
	{
		public System.String Name { get; } = CommandText.StartSettingsOne;

		//readonly InlineButtonSettings inlineButton = new InlineButtonSettings();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			SetUsers(_message, baseContext);
			SendingMessage(botClient, _message, "Привет друг мой, " + _message.From.FirstName, false);
		}
	}

	class StartOrganizer : ICommand
	{
		public System.String Name { get; } = CommandText.StartOrganizer;

		readonly InlineButtonOrganizer inlineButton = new InlineButtonOrganizer();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			baseContext._TempDate.Add(new TempDate() { user = users, date = System.DateTime.Today });
			try
			{
				await botClient.EditMessageTextAsync
					(
					_message.From.Id,
					_message.Message.MessageId,
					"Время работы: " + users.TimeToWork + "\nВремя отдыха: " + users.TimeToRelaxation,
					replyMarkup: inlineButton.StartMenu);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Exception: StartOrganizer : ICommand - " + ex);
			}
			baseContext.SaveChanges();
		}
	}
}
