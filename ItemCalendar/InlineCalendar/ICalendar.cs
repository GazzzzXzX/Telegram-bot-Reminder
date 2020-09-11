using System.Collections.Generic;
using Telegram.Bot;

namespace Calendar
{
	interface ICalendar
	{
		System.String Name { get; } //имя команды например /start, Привет, Пока

		void Execute(TelegramBotClient botClient, System.Object message, System.Object keyValues, DataBaseContext baseContext);//выполнение кода

		System.Boolean Equals(System.String CommandName);
	}
}