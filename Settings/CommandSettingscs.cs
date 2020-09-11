using System.Collections.Generic;
using System.Linq;
using Calendar.ItemSettings;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.Settings
{
	class CheckSettings
	{
		protected User users { get; set; }

		protected readonly InlineButtonSettings inlineButton = new InlineButtonSettings();

		protected void SetUsers(CallbackQuery message, DataBaseContext baseContext)
		{
			users = baseContext._User.Where(p => p.ID == message.From.Id).FirstOrDefault();
		}

		public CheckSettings()
		{
			users = null;
		}

		protected async void SendingMessage(TelegramBotClient botClient, CallbackQuery _message, System.String name, System.Boolean answer)
		{
			if(users.EmailAdress == null)
			{
				name += "\nE-Mail: не добавлено!";
			}
			else
			{
				name += "\nE-Mail: " + users.EmailAdress;
			}
			try
			{
				if(answer == true)
				{
					if(users.SendMessageE_Mail == true && users.SendMessageTelegram == false)
					{
						await botClient.EditMessageTextAsync
						(
						_message.From.Id,
						_message.Message.MessageId,
						name,
						replyMarkup: inlineButton.BackToSettingsThree
						);
					}
					else if(users.SendMessageE_Mail == true && users.SendMessageTelegram == true)
					{
						await botClient.EditMessageTextAsync
						(
						_message.From.Id,
						_message.Message.MessageId,
						name,
						replyMarkup: inlineButton.BackToSettingsTwo
						);
					}
					else if(users.SendMessageE_Mail == false && users.SendMessageTelegram == true)
					{
						await botClient.EditMessageTextAsync
						(
						_message.From.Id,
						_message.Message.MessageId,
						name,
						replyMarkup: inlineButton.BackToSettingsFour
						);
					}
					else if(users.SendMessageE_Mail == false && users.SendMessageTelegram == false)
					{
						await botClient.EditMessageTextAsync
						(
						_message.From.Id,
						_message.Message.MessageId,
						name,
						replyMarkup: inlineButton.BackToSettingsOne
						);
					}
				}
				else
				{
					if(users.SendMessageE_Mail == true && users.SendMessageTelegram == false)
					{
						await botClient.EditMessageTextAsync
						(
						_message.From.Id,
						_message.Message.MessageId,
						name,
						replyMarkup: inlineButton.StartSettingsThree
						);
					}
					else if(users.SendMessageE_Mail == true && users.SendMessageTelegram == true)
					{
						await botClient.EditMessageTextAsync
						(
						_message.From.Id,
						_message.Message.MessageId,
						name,
						replyMarkup: inlineButton.StartSettingsTwo
						);
					}
					else if(users.SendMessageE_Mail == false && users.SendMessageTelegram == true)
					{
						await botClient.EditMessageTextAsync
						(
						_message.From.Id,
						_message.Message.MessageId,
						name,
						replyMarkup: inlineButton.StartSettingsFour
						);
					}
					else if(users.SendMessageE_Mail == false && users.SendMessageTelegram == false)
					{
						await botClient.EditMessageTextAsync
						(
						_message.From.Id,
						_message.Message.MessageId,
						name,
						replyMarkup: inlineButton.StartSettingsOne
						);
					}
				}
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Exception: CheckSettings - " + ex);
			}
		}

	}

	class AddE_Mail : CheckSettings, ICommandSettings
	{
		public System.String Name { get; } = CommandText.AddE_Mail;

		//readonly InlineButtonSettings inlineButton = new InlineButtonSettings();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public void Execute(TelegramBotClient botClient, System.Object message, System.Object paris, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			SetUsers(_message, baseContext);

			users.Work = 3737;

			SendingMessage(botClient, _message, "Введите E-Mail: ", true);

			baseContext.SaveChanges();
		}
	}

	class DeleteE_Mail : ICommandSettings
	{
		public System.String Name { get; } = CommandText.DeleteE_Mail;

		readonly InlineButtonSettings inlineButton = new InlineButtonSettings();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object paris, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			try
			{
				await botClient.EditMessageTextAsync
						(
						_message.From.Id,
						_message.Message.MessageId,
						"Удалить E-Mail?",
						replyMarkup: inlineButton.DeleteE_Mail
						);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Exception: DeleteE_Mail : ICommandSettings - " + ex);
			}
		}
	}

	class YesDeleteE_Mail : CheckSettings, ICommandSettings
	{
		public System.String Name { get; } = CommandText.YesDeleteE_Mail;

		//readonly InlineButtonSettings inlineButton = new InlineButtonSettings();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public void Execute(TelegramBotClient botClient, System.Object message, System.Object paris, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			Dictionary<System.Int32, ISettings> iSettings = paris as Dictionary<System.Int32, ISettings>;

			SetUsers(_message, baseContext);

			users.Work = 3737;
			iSettings[users.Work % 100].Delete(botClient, _message, inlineButton, users, baseContext);

			SendingMessage(botClient, _message, "E-Mail был удален:", false);

			baseContext.SaveChanges();
		}
	}
	class NoDeleteE_Mail : CheckSettings, ICommandSettings
	{
		public System.String Name { get; } = CommandText.NoDeleteE_Mail;

		//readonly InlineButtonSettings inlineButton = new InlineButtonSettings();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public void Execute(TelegramBotClient botClient, System.Object message, System.Object paris, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			SetUsers(_message, baseContext);

			SendingMessage(botClient, _message, "E-Mail не был удален:", false);

			baseContext.SaveChanges();
		}
	}

	class SubscribeToTheNewsletterE_Mail : CheckSettings, ICommandSettings
	{
		public System.String Name { get; } = CommandText.SubscribeToTheNewsletterE_Mail;

		//readonly InlineButtonSettings inlineButton = new InlineButtonSettings();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public void Execute(TelegramBotClient botClient, System.Object message, System.Object paris, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			System.String name = "";

			SetUsers(_message, baseContext);
			if(users.SendMessageE_Mail == true)
			{
				users.SendMessageE_Mail = false;
				name = "\nВы отписались от рассылки на E-Mail.";
			}
			else
			{
				users.SendMessageE_Mail = true;
				name = "Вы подписались на рассылку на E-Mail.";
			}
			SendingMessage(botClient, _message, name, false);

			baseContext.SaveChanges();
		}
	}
	class SubscribeToTheNewsletterTelegram : CheckSettings, ICommandSettings
	{
		public System.String Name { get; } = CommandText.SubscribeToTheNewsletterTelegram;

		//readonly InlineButtonSettings inlineButton = new InlineButtonSettings();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public void Execute(TelegramBotClient botClient, System.Object message, System.Object paris, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			System.String name = "";
			SetUsers(_message, baseContext);
			if(users.SendMessageTelegram == true)
			{
				users.SendMessageTelegram = false;
				name = "Вы отписались от рассылки на telegram.";
			}
			else
			{
				users.SendMessageTelegram = true;
				name = "Вы подписались на рассылку на telegram.";
			}
			SendingMessage(botClient, _message, name, false);

			baseContext.SaveChanges();
		}
	}

	class BackToSettingsOne : ICommandSettings
	{
		public System.String Name { get; } = CommandText.BackToSettingsOne;

		readonly InlineButtonSettings inlineButton = new InlineButtonSettings();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object paris, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			try
			{
				await botClient.EditMessageTextAsync
					(
					_message.From.Id,
					_message.Message.MessageId,
					"Меню: ",
					replyMarkup: inlineButton.StartSettingsOne
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Exception: BackToSettingsOne : ICommandSettings - " + ex);
			}
		}
	}
	class BackToSettingsTwo : ICommandSettings
	{
		public System.String Name { get; } = CommandText.BackToSettingsTwo;

		readonly InlineButtonSettings inlineButton = new InlineButtonSettings();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object paris, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			try
			{
				await botClient.EditMessageTextAsync
					(
					_message.From.Id,
					_message.Message.MessageId,
					"Меню: ",
					replyMarkup: inlineButton.StartSettingsTwo
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Exception: BackToSettingsTwo : ICommandSettings - " + ex);
			}
		}
	}
	class BackToSettingsThree : ICommandSettings
	{
		public System.String Name { get; } = CommandText.BackToSettingsThree;

		readonly InlineButtonSettings inlineButton = new InlineButtonSettings();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object paris, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			try
			{
				await botClient.EditMessageTextAsync
					(
					_message.From.Id,
					_message.Message.MessageId,
					"Меню: ",
					replyMarkup: inlineButton.StartSettingsThree
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Exception: BackToSettingsThree : ICommandSettings - " + ex);
			}
		}
	}
	class BackToSettingsFour : ICommandSettings
	{
		public System.String Name { get; } = CommandText.BackToSettingsFour;

		readonly InlineButtonSettings inlineButton = new InlineButtonSettings();

		public System.Boolean Equals(System.String CommandName)
		{
			return Name.Equals(CommandName);
		}

		public async void Execute(TelegramBotClient botClient, System.Object message, System.Object paris, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			try
			{
				await botClient.EditMessageTextAsync
					(
					_message.From.Id,
					_message.Message.MessageId,
					"Меню: ",
					replyMarkup: inlineButton.StartSettingsFour
					);
			}
			catch(System.Exception ex)
			{
				System.Console.WriteLine("Exception: BackToSettingsFour : ICommandSettings - " + ex);
			}
		}
	}
}
