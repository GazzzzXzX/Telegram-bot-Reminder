using System;
using System.Collections.Generic;
using System.Text;
using Calendar.Command;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.Analyzer
{
	class AnalizeEach : Analize, IAnalize
	{
		public void SetName(TelegramBotClient BotClient, Message _message, User users, Object inlineButton, Object list, DataBaseContext baseContext)
		{
			InlineButtonCalendar inline = ChekIsNullInlineButtoncalendar(inlineButton);
			AddEachTime(BotClient, _message, users, inline, list, baseContext);
		}
	}
}
