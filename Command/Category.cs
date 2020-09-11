using System.Collections.Generic;
using System.Linq;
using Calendar.ItemCategory;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar
{
	class Category : ICommand
	{
		public System.String Name { get; } = CommandText.CategorySelection;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.Category);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Category : ICommand: " + ex);
			}
		}
	}

	class News : ICommand
	{
		public System.String Name { get; } = CommandText.News;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 1111;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.News);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("News : ICommand: " + ex);
			}
		}
	}

	class Sport : ICommand
	{
		public System.String Name { get; } = CommandText.Sport;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;


			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 1212;

			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.Sport);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Sport : ICommand: " + ex);
			}
		}
	}

	class Notes : ICommand
	{
		public System.String Name { get; } = CommandText.Notes;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;


			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 1313;

			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.Notes);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Notes : ICommand: " + ex);
			}
		}
	}

	class Cooking : ICommand
	{
		public System.String Name { get; } = CommandText.Cooking;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;


			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 1414;

			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.Cooking);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Cooking : ICommand: " + ex);
			}
		}
	}

	class Documents : ICommand
	{
		public System.String Name { get; } = CommandText.Documents;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;


			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 1515;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.Documents);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Documents : ICommand: " + ex);
			}
		}
	}

	class Video : ICommand
	{
		public System.String Name { get; } = CommandText.Video;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;


			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 1616;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.Video);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Video : ICommand: " + ex);
			}
		}
	}

	class Entertainment : ICommand
	{
		public System.String Name { get; } = CommandText.Entertainment;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;


			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 1717;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.Entertainment);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Entertainment : ICommand: " + ex);
			}
		}
	}

	class Finance : ICommand
	{
		public System.String Name { get; } = CommandText.Finance;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;


			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 1818;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.Finance);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Finance : ICommand: " + ex);
			}
		}
	}

	class Travels : ICommand
	{
		public System.String Name { get; } = CommandText.Travels;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;


			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 1919;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.Travels);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Travels : ICommand: " + ex);
			}
		}
	}

	class Cars : ICommand
	{
		public System.String Name { get; } = CommandText.Cars;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;


			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 2020;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.Cars);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Cars : ICommand: " + ex);
			}
		}
	}

	class Buy : ICommand
	{
		public System.String Name { get; } = CommandText.Buy;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;


			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 2121;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.Buy);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Buy : ICommand: " + ex);
			}
		}
	}

	class Anather : ICommand
	{
		public System.String Name { get; } = CommandText.Anather;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;


			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			users.Work = 2222;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.Anather);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Anather : ICommand: " + ex);
			}
		}
	}

	class BackToCategory : ICommand
	{
		public System.String Name { get; } = CommandText.BackToCategory;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			CommandText.keyValues.Clear();

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			try
			{
				itemCategories[users.Work % 100].SetNull(message, baseContext);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine(ex);
			}
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.Category);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("BackToCategory : ICommand: " + ex);
			}
		}
	}

	class NewsShow : ICommand
	{
		public System.String Name { get; } = CommandText.ShowItemCategory;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();

			System.Console.WriteLine("NewsShow " + users.Work);

			itemCategories[users.Work / 100].ShowItem(botClient, message, inlineButton, baseContext);

		}
	}

	class AddItemCategory : ICommand
	{
		public System.String Name { get; } = CommandText.AddItemCategory;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Введите заголовок: ", replyMarkup: inlineButton.ShowCategory);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("AddItemCategory : ICommand: " + ex);
			}
		}
	}

	class ChouseItem
	{
		readonly InlineButton inlineButton = new InlineButton();

		public void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategory, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();

			itemCategory[users.Work / 100].ChouseItem(botClient, message, inlineButton, baseContext);

		}
	}

	class DeleteNotes : ICommand
	{
		public System.String Name { get; } = CommandText.DeleteNotes;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();

			itemCategories[users.Work / 100].Delete(message, baseContext);
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.Category);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("DeleteNotes : ICommand: " + ex);
			}
		}
	}

	class ChangeFirst : ICommand
	{
		public System.String Name { get; } = CommandText.ChangeFirst;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			Message mes = message as Message;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();

			System.Console.WriteLine("\n\n" + users.Work / 100);

			itemCategories[users.Work / 100].ChangeFirstCat(message, baseContext);
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Заголовок: ", replyMarkup: inlineButton.BackToCategory);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ChangeFirst : ICommand: " + ex);
			}
		}
	}

	class ChangeSecond : ICommand
	{
		public System.String Name { get; } = CommandText.ChangeSecond;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			Message mes = message as Message;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			itemCategories[users.Work / 100].ChangeSecondCat(message, baseContext);
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Содержание: ", replyMarkup: inlineButton.BackToCategory);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("ChangeSecond : ICommand: " + ex);
			}
		}
	}

	class BackToNews : ICommand
	{
		public System.String Name { get; } = CommandText.BackToNews;

		readonly InlineButton inlineButton = new InlineButton();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();

			itemCategories[users.Work / 100].BackToNews(message, baseContext);
			try
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Меню: ", replyMarkup: inlineButton.News);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("BackToNews : ICommand: " + ex);
			}
		}

	}
}
