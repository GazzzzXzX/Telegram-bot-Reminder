using System.Collections.Generic;
using Calendar.Command;
using Calendar.ItemCalendar;
using Calendar.ItemCategory;
using Calendar.ItemOrganizer;
using Calendar.ItemSettings;
using Calendar.OrganizerTelegram;
using Calendar.Settings;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.Analyzer
{
	interface IAnalize
	{
		void SetName(TelegramBotClient BotClient, Message _message, User users, System.Object inlineButton, System.Object list, DataBaseContext baseContext);
	}

	class Analize
	{
		protected InlineButton ChekIsNullInlineButton(System.Object inlineBytton)
		{
			return inlineBytton as InlineButton;
		}

		protected InlineButtonCalendar ChekIsNullInlineButtoncalendar(System.Object inlineBytton)
		{
			return inlineBytton as InlineButtonCalendar;
		}

		protected InlineButtonSettings ChekIsNullInlineButtonSettings(System.Object inlineButton)
		{
			return inlineButton as InlineButtonSettings;
		}

		protected InlineButtonOrganizer ChekIsNullInlineButtonOrganizer(System.Object inlineButton)
		{
			return inlineButton as InlineButtonOrganizer;
		}

		protected void SetNameCal(TelegramBotClient BotClient, Message _message, User users, InlineButtonCalendar inlineButton, System.Object list, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, IEventCal> items = list as Dictionary<System.Int32, IEventCal>;
			items[users.Work % 100].Name = _message.Text;
			items[users.Work % 100].SetName(BotClient, _message, inlineButton, users, baseContext);
		}

		protected void SetContextCal(TelegramBotClient BotClient, Message _message, User users, InlineButtonCalendar inlineButton, System.Object list, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, IEventCal> items = list as Dictionary<System.Int32, IEventCal>;
			items[users.Work % 100].SetContext(BotClient, _message, inlineButton, baseContext);
		}

		protected void AddTimeCal(TelegramBotClient BotClient, Message _message, User users, InlineButtonCalendar inlineButton, System.Object list, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, IEventCal> items = list as Dictionary<System.Int32, IEventCal>;
			items[users.Work % 100].SetTime(BotClient, _message, inlineButton, users, baseContext);
		}

		protected void AddLocationCal(TelegramBotClient BotClient, Message _message, User users, InlineButtonCalendar inlineButton, System.Object list, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, IEventCal> items = list as Dictionary<System.Int32, IEventCal>;
			items[users.Work % 100].SetLocation(BotClient, _message, inlineButton, baseContext);
		}

		protected void ChangeNameCal(TelegramBotClient BotClient, Message _message, User users, InlineButtonCalendar inlineButton, System.Object list, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, IEventCal> items = list as Dictionary<System.Int32, IEventCal>;
			items[users.Work % 100].ChangeName(BotClient, _message, inlineButton, baseContext);
		}

		protected void ChangeNoteCal(TelegramBotClient BotClient, Message _message, User users, InlineButtonCalendar inlineButton, System.Object list, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, IEventCal> items = list as Dictionary<System.Int32, IEventCal>;
			items[users.Work % 100].ChangeNote(BotClient, _message, inlineButton, baseContext);
		}

		protected void ChangeTimeCal(TelegramBotClient BotClient, Message _message, User users, InlineButtonCalendar inlineButton, System.Object list, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, IEventCal> items = list as Dictionary<System.Int32, IEventCal>;
			items[users.Work % 100].ChangeTime(BotClient, _message, inlineButton, baseContext);
		}

		protected void ChangeLocationCal(TelegramBotClient BotClient, Message _message, User users, InlineButtonCalendar inlineButton, System.Object list, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, IEventCal> items = list as Dictionary<System.Int32, IEventCal>;
			items[users.Work % 100].ChangeLocation(BotClient, _message, inlineButton, baseContext);
		}

		protected void SetNameCat(TelegramBotClient BotClient, Message _message, User users, InlineButton inlineButton, System.Object list, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, IItemCategory> items = list as Dictionary<System.Int32, IItemCategory>;
			items[users.Work % 100].Name = _message.Text;
			items[users.Work % 100].SetName(BotClient, _message, inlineButton, users, baseContext);
		}

		protected void SetContextCat(TelegramBotClient BotClient, Message _message, User users, InlineButton inlineButton, System.Object list, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, IItemCategory> items = list as Dictionary<System.Int32, IItemCategory>;
			items[users.Work % 100].SetContent(BotClient, _message, inlineButton, baseContext);
		}

		protected void ChangeFirstCat(TelegramBotClient BotClient, Message _message, User users, InlineButton inlineButton, System.Object list, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, IItemCategory> items = list as Dictionary<System.Int32, IItemCategory>;
			items[users.Work % 100].ChangeFirst(BotClient, _message, inlineButton, users, baseContext);
		}

		protected void ChangeSecondCat(TelegramBotClient BotClient, Message _message, User users, InlineButton inlineButton, System.Object list, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, IItemCategory> items = list as Dictionary<System.Int32, IItemCategory>;
			items[users.Work % 100].ChangeSecond(BotClient, _message, inlineButton, users, baseContext);
		}

		protected void AddEachTime(TelegramBotClient BotClient, Message _message, User users, InlineButtonCalendar inlineButton, System.Object list, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, IEvetnEach> items = list as Dictionary<System.Int32, IEvetnEach>;
			items[users.Work % 100].SetTime(BotClient, _message, inlineButton, users, baseContext);

		}

		protected void AddE_Mail(TelegramBotClient botClient, Message _message, User users, System.Object list, InlineButtonSettings inlineButton, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, ISettings> items = list as Dictionary<System.Int32, ISettings>;
			items[users.Work % 100].SetE_Mail(botClient, _message, inlineButton, users, baseContext);
		}

		protected void AddTimeOrganizer(TelegramBotClient botClient, Message _message, User users, System.Object list, InlineButtonOrganizer inlineButton, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, IItemOrganizer> items = list as Dictionary<System.Int32, IItemOrganizer>;
			items[users.Work / 100].SetTimeWork(botClient, _message, inlineButton, users, baseContext);
		}

		protected void AddTimeRelaxOrganizer(TelegramBotClient botClient, Message _message, User users, System.Object list, InlineButtonOrganizer inlineButton, DataBaseContext baseContext)
		{
			Dictionary<System.Int32, IItemOrganizer> items = list as Dictionary<System.Int32, IItemOrganizer>;
			items[(users.Work / 100) - 1].SetTimeRelaxation(botClient, _message, inlineButton, users, baseContext);
		}
	}
}
