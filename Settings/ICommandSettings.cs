using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;

namespace Calendar.Settings
{
	interface ICommandSettings
	{
		System.String Name { get; } //имя команды например /start, Привет, Пока

		void Execute(TelegramBotClient botClient, System.Object message, System.Object paris, DataBaseContext baseContext);//выполнение кода

		System.Boolean Equals(System.String CommandName);
	}
}
