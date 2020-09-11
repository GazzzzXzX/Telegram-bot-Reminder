using System;
using System.Collections.Generic;
using System.Text;
using Calendar.Settings;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.Analyzer
{
	class AnalizeE_Mail : Analize, IAnalize
	{
		public void SetName(TelegramBotClient BotClient, Message _message, User users, Object inlineButton, Object list, DataBaseContext baseContext)
		{
			InlineButtonSettings inline = ChekIsNullInlineButtonSettings(inlineButton);
			AddE_Mail(BotClient, _message, users, list, inline, baseContext);
		}
	}
}
