using System;
using Calendar.Command;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.Analyzer
{
	class AnalizChangeTime : Analize, IAnalize
	{
		public void SetName(TelegramBotClient BotClient, Message _message, User users, Object inlineButton, Object list, DataBaseContext baseContext)
		{
			InlineButtonCalendar inline = ChekIsNullInlineButtoncalendar(inlineButton);
			ChangeTimeCal(BotClient, _message, users, inline, list, baseContext);
		}
	}
}
