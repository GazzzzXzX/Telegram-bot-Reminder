using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Calendar.ItemOrganizer;
using Calendar.Picture;
using Calendar.SQL;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.OrganizerTelegram
{
	class StartOrganizerTime : IOrganizer
	{
		public System.String Name { get; } = CommandText.StartOrganizerTime;

		readonly InlineButtonOrganizer inlineButton = new InlineButtonOrganizer();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemOrganizer> pairs, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
		
			
			pairs[38].StartWork(botClient, message, inlineButton, users, baseContext);
			
			try
			{
				await botClient.EditMessageTextAsync
					(
					_message.From.Id,
					_message.Message.MessageId,
					"Время работы: " + users.TimeToWork + "\nВремя отдыха: " + users.TimeToRelaxation,
					replyMarkup: inlineButton.BackToOrganizer);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("StartOrganizerTime : IOrganizer: " + ex);
			}
			baseContext.SaveChanges();
		}
	}

	class BackToOrganizer : IOrganizer
	{
		public System.String Name { get; } = CommandText.BackToOrganizer;

		readonly InlineButtonOrganizer inlineButton = new InlineButtonOrganizer();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemOrganizer> pairs, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			try
			{
				await botClient.EditMessageTextAsync
					(
					_message.From.Id,
					_message.Message.MessageId,
					"Время работы: " + users.TimeToWork + "\nВремя отдыха: " + users.TimeToRelaxation,
					replyMarkup: inlineButton.StartMenu);
			}
			catch
			{
				await botClient.SendTextMessageAsync
					(
					_message.From.Id,
					"Время работы: " + users.TimeToWork + "\nВремя отдыха: " + users.TimeToRelaxation,
					replyMarkup: inlineButton.StartMenu
					);
			}
		}
	}

	class OrganizerTimeToWork : IOrganizer
	{
		public System.String Name { get; } = CommandText.OrganizerTimeToWork;

		readonly InlineButtonOrganizer inlineButton = new InlineButtonOrganizer();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemOrganizer> pairs, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();

			users.Work = 3800;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync
					(
					_message.From.Id,
					_message.Message.MessageId,
					"Время работы: " + users.TimeToWork + "\nВремя отдыха: " + users.TimeToRelaxation,
					replyMarkup: inlineButton.BackToOrganizer);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("OrganizerTimeToWork : IOrganizer: " + ex);
			}
		}
	}

	class OrganizerTimeToRelaxation : IOrganizer
	{
		public System.String Name { get; } = CommandText.OrganizerTimeToRelaxation;

		readonly InlineButtonOrganizer inlineButton = new InlineButtonOrganizer();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemOrganizer> pairs, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();

			users.Work = 3900;
			baseContext.SaveChanges();
			try
			{
				await botClient.EditMessageTextAsync
					(
					_message.From.Id,
					_message.Message.MessageId,
					"Время работы: " + users.TimeToWork + "\nВремя отдыха: " + users.TimeToRelaxation,
					replyMarkup: inlineButton.BackToOrganizer);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("OrganizerTimeToRelaxation : IOrganizer: " + ex);
			}
		}
	}

	class OrganizerSattisticMouth : IOrganizer
	{
		public System.String Name { get; } = CommandText.OrganizerSattisticMouth;

		readonly InlineButtonOrganizer inlineButton = new InlineButtonOrganizer();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemOrganizer> pairs, DataBaseContext baseContext)
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
			picture.SetPicture(users, _message, statistics, date, baseContext);

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
				await botClient.SendPhotoAsync(_message.From.Id, stream, replyMarkup: inlineButton.BackToOrganizer);
			}
			catch
			{
				await botClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Нет статистики за этот день.", replyMarkup: inlineButton.StartMenu);
			}
			stream.Close();

			if(System.IO.File.Exists(str))
			{
				// If file found, delete it    
				System.IO.File.Delete(str);
			}

		}
	}

	class StartOrganizerTimeRelax : IOrganizer
	{
		public System.String Name { get; } = CommandText.StartOrganizerTimeRelax;

		readonly InlineButtonOrganizer inlineButton = new InlineButtonOrganizer();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemOrganizer> pairs, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			pairs[38].StartRelax(botClient, message, inlineButton, users, baseContext);
			try
			{
				await botClient.EditMessageTextAsync
					(
					_message.From.Id,
					_message.Message.MessageId,
					"Время работы: " + users.TimeToWork + "\nВремя отдыха: " + users.TimeToRelaxation,
					replyMarkup: inlineButton.BackToOrganizer);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("StartOrganizerTimeRelax : IOrganizer: " + ex);
			}
		}
	}

}
