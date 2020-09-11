using Telegram.Bot.Types.ReplyMarkups;

namespace Calendar.OrganizerTelegram
{
	class InlineButtonOrganizer
	{
		public InlineKeyboardMarkup StartMenu = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Старт", CallbackData = CommandText.StartOrganizerTime },
				new InlineKeyboardButton{ Text = "Время работы", CallbackData = CommandText.OrganizerTimeToWork}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Время отдыха", CallbackData = CommandText.OrganizerTimeToRelaxation},
				new InlineKeyboardButton{ Text = "Статистика за сегодня", CallbackData = CommandText.OrganizerSattisticMouth}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToStart}
			}
		};

		public InlineKeyboardMarkup BackToOrganizer = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToOrganizer}
			}
		};

		public InlineKeyboardMarkup StartOrganizerTimeRelax = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Дота не дремлет", CallbackData = CommandText.StartOrganizerTimeRelax}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToOrganizer}
			}
		};

		public InlineKeyboardMarkup StartOrganizerTime = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Эх... опять работать", CallbackData = CommandText.StartOrganizerTime}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToOrganizer}
			}
		};

		public InlineKeyboardMarkup StartMenuStop = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Остановать", CallbackData = CommandText.StopOrganizerTime },
				new InlineKeyboardButton{ Text = "Время работы", CallbackData = CommandText.OrganizerTimeToWork}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Время отдыха", CallbackData = CommandText.OrganizerTimeToRelaxation},
				new InlineKeyboardButton{ Text = "Статистика за сегодня", CallbackData = CommandText.OrganizerSattisticMouth}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToStart}
			}
		};
	}
}
