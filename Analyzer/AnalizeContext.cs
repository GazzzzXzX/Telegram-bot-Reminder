﻿using System;
using Calendar.Command;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.Analyzer
{
	class AnalizeContext : Analize, IAnalize
	{
		public void SetName(TelegramBotClient BotClient, Message _message, User users, Object inlineButton, Object list, DataBaseContext baseContext)
		{
			if(ChekIsNullInlineButton(inlineButton) != null)
			{
				InlineButton inline = ChekIsNullInlineButton(inlineButton);
				SetContextCat(BotClient, _message, users, inline, list, baseContext);
			}
			else
			{
				InlineButtonCalendar inline = ChekIsNullInlineButtoncalendar(inlineButton);
				SetContextCal(BotClient, _message, users, inline, list, baseContext);
			}
		}
	}
}
