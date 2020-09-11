using System;
using System.Collections.Generic;
using System.Text;
using Calendar.Command;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.ItemCalendar
{
	class AddEachDay : IEvetnEach
	{
		public void ChangeTime(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, User users, DataBaseContext baseContext)
		{

		}

		public void DeleteTime(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, User users, DataBaseContext baseContext)
		{

		}

		public void SetTime(TelegramBotClient BotClient, Message _message, InlineButtonCalendar inlineButton, User users, DataBaseContext baseContext)
		{
			System.Console.WriteLine("AddEachDay");
			//foreach()
		}
	}
}
