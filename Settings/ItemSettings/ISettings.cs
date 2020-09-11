using Calendar.Settings;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.ItemSettings
{
	interface ISettings
	{
		void SetE_Mail(TelegramBotClient BotClient, Message message, InlineButtonSettings inlineButton, User users, DataBaseContext baseContext);
		void Delete(TelegramBotClient BotClient, CallbackQuery message, InlineButtonSettings inlineButton, User users, DataBaseContext baseContext);
	}
}
