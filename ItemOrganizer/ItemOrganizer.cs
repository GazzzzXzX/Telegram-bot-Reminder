using Calendar.OrganizerTelegram;
using Calendar.Settings;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.ItemOrganizer
{
	interface IItemOrganizer
	{
		void SetTimeWork(TelegramBotClient BotClient, System.Object message, InlineButtonOrganizer inlineButton, User users, DataBaseContext baseContext);
		void SetTimeRelaxation(TelegramBotClient BotClient, System.Object message, InlineButtonOrganizer inlineButton, User users, DataBaseContext baseContext);
		void ShowTime(TelegramBotClient BotClient, System.Object message, InlineButtonOrganizer inlineButton, User users, DataBaseContext baseContext);
		void StartWork(TelegramBotClient botClient, System.Object message, InlineButtonOrganizer inlineButton, User users, DataBaseContext baseContext);
		void StartRelax(TelegramBotClient botClient, System.Object message, InlineButtonOrganizer inlineButton, User users, DataBaseContext baseContext);
	}
}
