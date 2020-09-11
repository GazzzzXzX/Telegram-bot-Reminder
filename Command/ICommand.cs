using System.Collections.Generic;
using Calendar.ItemCategory;
using Telegram.Bot;

namespace Calendar
{
	interface ICommand
	{

		System.String Name { get; } //имя команды например /start, Привет, Пока

		void Execute(TelegramBotClient botClient, System.Object message, Dictionary<System.Int32, IItemCategory> itemCategories, DataBaseContext baseContext);//выполнение кода

		System.Boolean Equals(System.String CommandName);


	}
}
