using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.ItemCategory
{
	class NothingCat : IItemCategory
	{
		public System.String Name { get; set; }

		public void Delete(System.Object message, DataBaseContext baseContext)
		{

		}

		public void BackToNews(System.Object message, DataBaseContext baseContext)
		{

		}

		public void ChouseItem(TelegramBotClient BotClient, System.Object message, InlineButton inlineButton, DataBaseContext baseContext)
		{ }

		public void SetContent(TelegramBotClient BotClient, Message _message, InlineButton inlineButton, DataBaseContext baseContext)
		{

		}

		public void SetName(TelegramBotClient BotClient, Message _message, InlineButton inlineButton, User users, DataBaseContext baseContext)
		{

		}

		public void ShowItem(TelegramBotClient BotClient, System.Object message, InlineButton inlineButton, DataBaseContext baseContext)
		{

		}

		public void ChangeFirst(TelegramBotClient BotClient, Message _message, InlineButton inlineButton, User users, DataBaseContext baseContext)
		{

		}

		public void ChangeSecond(TelegramBotClient BotClient, Message _message, InlineButton inlineButton, User users, DataBaseContext baseContext)
		{
		}

		public void ChangeFirstCat(System.Object message, DataBaseContext baseContext)
		{
		}

		public void ChangeSecondCat(System.Object message, DataBaseContext baseContext)
		{
		}

		public void SetNull(System.Object message, DataBaseContext baseContext)
		{
		}
	}
}