using System.Linq;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.ItemCategory
{
	class AnatherCat : IItemCategory
	{
		public System.String Name { get; set; }

		public void Delete(System.Object message, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			foreach(BDAnather anather in baseContext._Anather.Include("user"))
			{
				if(anather.user.ID == _message.From.Id && anather.Work == true)
				{
					baseContext._Anather.Remove(anather);
				}
			}
			baseContext.SaveChanges();
		}

		public void BackToNews(System.Object message, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			foreach(BDAnather anather in baseContext._Anather.Include("user"))
			{
				if(anather.user.ID == _message.From.Id && anather.Work == true)
				{
					anather.Work = false;
				}
			}
			baseContext.SaveChanges();
		}

		public async void ChouseItem(TelegramBotClient BotClient, System.Object message, InlineButton inlineButton, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			Name = _message.Data;
			System.String[] words = Name.Split(new System.Char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
			System.Int32 temp = System.Convert.ToInt32(words[0]);

			BDAnather news = baseContext._Anather.Where(p => p.ID == temp).FirstOrDefault();

			news.Work = true;

			baseContext.SaveChanges();

			await BotClient.EditMessageTextAsync
				(
				_message.From.Id,
				_message.Message.MessageId, 
				"Название: " + news.Name + "\nСодержание:\n" + news.link, 
				replyMarkup: inlineButton.ControlItem
				);
		}

		public async void SetContent(TelegramBotClient BotClient, Message _message, InlineButton inlineButton, DataBaseContext baseContext)
		{
			foreach(BDAnather temp in baseContext._Anather.Include("user"))
			{
				if(temp.user.ID == _message.From.Id && temp.Work == true)
				{
					temp.link = _message.Text;
					temp.Work = false;
				}
			}
			await BotClient.SendTextMessageAsync(_message.Chat.Id, "Запись добавлена!\n" + "Заголовок: " + Name + "\nСодержание: " + _message.Text, replyMarkup: inlineButton.ShowCategory);

			baseContext.SaveChanges();
		}

		public async void SetName(TelegramBotClient BotClient, Message _message, InlineButton inlineButton, User users, DataBaseContext baseContext)
		{
			users.Work = 2322;
			baseContext._Anather.Add(new BDAnather() { Name = Name, user = users, Work = true });
			await BotClient.SendTextMessageAsync(_message.Chat.Id, "Заголовок: " + Name + "\nВведите содержание: ", replyMarkup: inlineButton.ShowCategory);
			baseContext.SaveChanges();
		}

		public async void ShowItem(TelegramBotClient BotClient, System.Object message, InlineButton inlineButton, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup answer = inlineButton.AnatherShow(message, baseContext);
			await BotClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Выберете статью: ", replyMarkup: answer);
		}

		public async void ChangeFirst(TelegramBotClient BotClient, Message _message, InlineButton inlineButton, User users, DataBaseContext baseContext)
		{
			foreach(BDAnather news in baseContext._Anather.Include("user"))
			{
				if(news.user.ID == _message.From.Id && news.Work == true)
				{
					news.Name = _message.Text;

					await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nНовый заголовок: " + news.Name, replyMarkup: inlineButton.Anather);
					news.user.Work = 2222;
					news.Work = false;

				}
			}
		}

		public async void ChangeSecond(TelegramBotClient BotClient, Message _message, InlineButton inlineButton, User users, DataBaseContext baseContext)
		{
			foreach(BDAnather temp in baseContext._Anather.Include("user"))
			{
				if(temp.user.ID == _message.From.Id && temp.Work == true)
				{
					temp.link = _message.Text;

					await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nНовое содержание: " + temp.link, replyMarkup: inlineButton.Anather);
					temp.user.Work = 2222;
					temp.Work = false;
				}
			}
			baseContext.SaveChanges();
		}

		public void ChangeFirstCat(System.Object message, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			foreach(BDAnather temp in baseContext._Anather.Include("user"))
			{
				if(temp.user.ID == _message.From.Id && temp.Work == true)
				{
					temp.user.Work = 2422;
				}
			}
		}

		public void ChangeSecondCat(System.Object message, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			foreach(BDAnather temp in baseContext._Anather.Include("user"))
			{
				if(temp.user.ID == _message.From.Id && temp.Work == true)
				{
					temp.user.Work = 2522;
				}
			}
		}

		public void SetNull(System.Object message, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			foreach(BDAnather temp in baseContext._Anather.Include("user"))
			{
				if(temp.user.ID == _message.From.Id)
				{
					temp.user.Work = 0;
					if(temp.Work == true)
					{
						temp.Work = false;
					}
				}
			}
			baseContext.SaveChanges();
		}
	}
}
