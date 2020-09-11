using System;
using Calendar.Command;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.Analyzer
{
	class AnalizChangeLocation : Analize, IAnalize
	{
		public void SetName(TelegramBotClient BotClient, Message _message, User users, Object inlineButton, Object list, DataBaseContext baseContext)
		{
			InlineButtonCalendar inline = ChekIsNullInlineButtoncalendar(inlineButton);
			ChangeLocationCal(BotClient, _message, users, inline, list, baseContext);
		}
	}
}