using Telegram.Bot.Types.ReplyMarkups;

namespace Calendar.Settings
{
	class InlineButtonSettings
	{
		
		public InlineKeyboardMarkup StartSettingsOne = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Добавить e-mail", CallbackData = CommandText.AddE_Mail}

			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Удалить e-mail", CallbackData = CommandText.DeleteE_Mail }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Подписаться на рассылку на e-mail", CallbackData = CommandText.SubscribeToTheNewsletterE_Mail}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Подписаться на рассылку в telegram", CallbackData = CommandText.SubscribeToTheNewsletterTelegram}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToStart}
			},
		};
		public InlineKeyboardMarkup StartSettingsTwo = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Добавить e-mail", CallbackData = CommandText.AddE_Mail}

			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Удалить e-mail", CallbackData = CommandText.DeleteE_Mail }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Отписаться от рассылки на e-mail", CallbackData = CommandText.SubscribeToTheNewsletterE_Mail},

			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Отписаться от рассылки в telegram", CallbackData = CommandText.SubscribeToTheNewsletterTelegram}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToStart}
			},
		};
		public InlineKeyboardMarkup StartSettingsThree = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Добавить e-mail", CallbackData = CommandText.AddE_Mail}

			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Удалить e-mail", CallbackData = CommandText.DeleteE_Mail }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Отписаться от рассылки на e-mail", CallbackData = CommandText.SubscribeToTheNewsletterE_Mail}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Подписаться на рассылку в telegram", CallbackData = CommandText.SubscribeToTheNewsletterTelegram}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToStart}
			},
		};
		public InlineKeyboardMarkup StartSettingsFour = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Добавить e-mail", CallbackData = CommandText.AddE_Mail}

			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Удалить e-mail", CallbackData = CommandText.DeleteE_Mail }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Подписаться на рассылку на e-mail", CallbackData = CommandText.SubscribeToTheNewsletterE_Mail}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Отписаться от рассылки в telegram", CallbackData = CommandText.SubscribeToTheNewsletterTelegram}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToStart}
			},
		};

		public InlineKeyboardMarkup BackToSettingsOne = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Назад", CallbackData = CommandText.BackToSettingsOne }
			}
		};
		public InlineKeyboardMarkup BackToSettingsTwo = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Назад", CallbackData = CommandText.BackToSettingsTwo }
			}
		};
		public InlineKeyboardMarkup BackToSettingsThree = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Назад", CallbackData = CommandText.BackToSettingsThree }
			}
		};
		public InlineKeyboardMarkup BackToSettingsFour = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Назад", CallbackData = CommandText.BackToSettingsFour }
			}
		};

		public InlineKeyboardMarkup DeleteE_Mail = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Да", CallbackData = CommandText.YesDeleteE_Mail},
				new InlineKeyboardButton { Text = "Нет", CallbackData = CommandText.NoDeleteE_Mail}
			}

		};

	}
}
