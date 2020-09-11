using System.Collections.Generic;
using Calendar.ItemCategory;
using Calendar.ItemOrganizer;
using Telegram.Bot;

namespace Calendar.OrganizerTelegram
{
	interface IOrganizer
	{
		System.String Name { get; } //имя команды например /start, Привет, Пока

		void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemOrganizer> pairs, DataBaseContext baseContext);//выполнение кода

		System.Boolean Equals(System.String CommandName);
	}
}
