using System;
using Calendar.Command;
using Calendar.OrganizerTelegram;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.Analyzer
{
	class AnalizeTimeOrganizer : Analize, IAnalize
	{
		public void SetName(TelegramBotClient BotClient, Message _message, User users, Object inlineButton, Object list, DataBaseContext baseContext)
		{
			InlineButtonOrganizer inline = ChekIsNullInlineButtonOrganizer(inlineButton);
			if((users.Work / 100) == 38)
			{
				AddTimeOrganizer(BotClient, _message, users, list, inline, baseContext);
			}
			else if((users.Work / 100) == 39)
			{
				AddTimeRelaxOrganizer(BotClient, _message, users, list, inline, baseContext);
			}
		}
	}
}
