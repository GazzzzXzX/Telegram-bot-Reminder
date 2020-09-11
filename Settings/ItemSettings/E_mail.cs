using System.Globalization;
using System.Text.RegularExpressions;
using Calendar.Settings;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.ItemSettings
{
	class E_mail : CheckSettings, ISettings
	{
		private System.Boolean IsValidEmail(System.String email)
		{
			if(System.String.IsNullOrWhiteSpace(email))
			{
				return false;
			}

			try
			{
				// Normalize the domain
				email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
									  RegexOptions.None, System.TimeSpan.FromMilliseconds(200));

				// Examines the domain part of the email and normalizes it.
				System.String DomainMapper(Match match)
				{
					// Use IdnMapping class to convert Unicode domain names.
					var idn = new IdnMapping();

					// Pull out and process domain name (throws ArgumentException on invalid)
					var domainName = idn.GetAscii(match.Groups[2].Value);

					return match.Groups[1].Value + domainName;
				}
			}
			catch(RegexMatchTimeoutException e)
			{
				return false;
			}
			catch(System.ArgumentException e)
			{
				return false;
			}

			try
			{
				return Regex.IsMatch(email,
					@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
					@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
					RegexOptions.IgnoreCase, System.TimeSpan.FromMilliseconds(250));
			}
			catch(RegexMatchTimeoutException)
			{
				return false;
			}
		}

		private async void SendingMessage(TelegramBotClient BotClient, Message _message, InlineButtonSettings inlineButton, User users)
		{
			if(users.SendMessageE_Mail == true && users.SendMessageTelegram == false)
			{
				await BotClient.SendTextMessageAsync
					(
					_message.Chat.Id,
					"E-Mail: " + _message.Text,
					replyMarkup: inlineButton.StartSettingsThree
					);
			}
			else if(users.SendMessageE_Mail == true && users.SendMessageTelegram == true)
			{
				await BotClient.SendTextMessageAsync
					(
					_message.Chat.Id,
					"E-Mail: " + _message.Text,
					replyMarkup: inlineButton.StartSettingsTwo
				);
			}
			else if(users.SendMessageE_Mail == false && users.SendMessageTelegram == true)
			{
				await BotClient.SendTextMessageAsync
					(
					_message.Chat.Id,
					"E-Mail: " + _message.Text,
					replyMarkup: inlineButton.StartSettingsFour
				);
			}
			else if(users.SendMessageE_Mail == false && users.SendMessageTelegram == false)
			{
				await BotClient.SendTextMessageAsync
					(
					_message.Chat.Id,
					"E-Mail: " + _message.Text,
					replyMarkup: inlineButton.StartSettingsOne
				);
			}
		}

		public async void SetE_Mail(TelegramBotClient BotClient, Message _message, InlineButtonSettings inlineButton, User users, DataBaseContext baseContext)
		{
			if(IsValidEmail(_message.Text))
			{
				users.EmailAdress = _message.Text;
				users.SendMessageE_Mail = true;
				users.Work = 0;
				baseContext.SaveChanges();

				SendingMessage(BotClient, _message, inlineButton, users);
			}
			else
			{
				await BotClient.SendTextMessageAsync
				(
				_message.Chat.Id,
				"E-Mail введен не верно.\nВведите E-Mail: ",
				replyMarkup: inlineButton.StartSettingsFour
				);
			}
		}

		public void Delete(TelegramBotClient BotClient, CallbackQuery _message, InlineButtonSettings inlineButton, User users, DataBaseContext baseContext)
		{
			users.EmailAdress = null;
			users.SendMessageE_Mail = false;

			baseContext.SaveChanges();
		}
	}
}
