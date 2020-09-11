using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.ItemCategory
{
	interface IItemCategory
	{

		System.String Name { get; set; }

		void SetName(TelegramBotClient BotClient, Message _message, InlineButton inlineButton, User users, DataBaseContext baseContext);
		void SetContent(TelegramBotClient BotClient, Message _message, InlineButton inlineButton, DataBaseContext baseContext);
		void ChouseItem(TelegramBotClient BotClient, System.Object message, InlineButton inlineButton, DataBaseContext baseContext);
		void ShowItem(TelegramBotClient BotClient, System.Object message, InlineButton inlineButton, DataBaseContext baseContext);
		void BackToNews(System.Object message, DataBaseContext baseContext);
		void Delete(System.Object message, DataBaseContext baseContext);
		void SetNull(System.Object message, DataBaseContext baseContext);
		void ChangeFirst(TelegramBotClient BotClient, Message _message, InlineButton inlineButton, User users, DataBaseContext baseContext);
		void ChangeSecond(TelegramBotClient BotClient, Message _message, InlineButton inlineButton, User users, DataBaseContext baseContext);

		void ChangeFirstCat(System.Object message, DataBaseContext baseContext);
		void ChangeSecondCat(System.Object message, DataBaseContext baseContext);
	}
}
