using Telegram.Bot;
using Telegram.Bot.Types;
using Calendar.Command;
namespace Calendar.ItemCalendar
{
	interface IEventCal
	{
		System.String Name { get; set; }
		void SetName(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, User users, DataBaseContext baseContext);
		void SetContext(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext);
		void SetTime(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, User users, DataBaseContext baseContext);
		void SetFalse(CallbackQuery _message, DataBaseContext baseContext);
		void SetLocation(TelegramBotClient BotClient, Message message, InlineButtonCalendar inlineButton, DataBaseContext baseContext);
		void ShowItem(TelegramBotClient BotClient, System.Object message, InlineButtonCalendar inlineButton, DataBaseContext baseContext);
		void ChangeName(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext);
		void ChangeNote(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext);
		void Dalete(CallbackQuery _message, DataBaseContext baseContext);
		void ChangeTime(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext);
		void ChangeLocation(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, DataBaseContext baseContext);
	}

	interface IEvetnEach
	{
		void SetTime(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, User users, DataBaseContext baseContext);
		void DeleteTime(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, User users, DataBaseContext baseContext);
		void ChangeTime(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, User users, DataBaseContext baseContext);
	}
}
