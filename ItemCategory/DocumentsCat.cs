using System.Linq;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar.ItemCategory
{
	class DocumentsCat : IItemCategory
	{
		public System.String Name { get; set; }

		public void Delete(System.Object message, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			foreach(BDDocuments documents in baseContext._Documets.Include("user"))
			{
				if(documents.user.ID == _message.From.Id && documents.Work == true)
				{
					baseContext._Documets.Remove(documents);
				}
			}
			baseContext.SaveChanges();
		}

		public void BackToNews(System.Object message, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			foreach(BDDocuments documents in baseContext._Documets.Include("user"))
			{
				if(documents.user.ID == _message.From.Id && documents.Work == true)
				{
					documents.Work = false;
				}
			}
			baseContext.SaveChanges();
		}

		public async void ChouseItem(TelegramBotClient BotClient, System.Object message, InlineButton inlineButton, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			Name = _message.Data;
			string[] words = Name.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
			System.Int32 temp = System.Convert.ToInt32(words[0]);

			BDDocuments news = baseContext._Documets.Where(p => p.ID == temp).FirstOrDefault();

			news.Work = true;

			baseContext.SaveChanges();

			await BotClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Название: " + news.Name + "\nСодержание:\n" + news.link, replyMarkup: inlineButton.ControlItem);
		}

		public async void SetContent(TelegramBotClient BotClient, Message _message, InlineButton inlineButton, DataBaseContext baseContext)
		{
			foreach(BDDocuments temp in baseContext._Documets.Include("user"))
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
			users.Work = 2315;
			baseContext._Documets.Add(new BDDocuments() { Name = Name, user = users, Work = true });
			await BotClient.SendTextMessageAsync(_message.Chat.Id, "Заголовок: " + Name + "\nВведите содержание: ", replyMarkup: inlineButton.ShowCategory);
			baseContext.SaveChanges();
		}

		public async void ShowItem(TelegramBotClient BotClient, System.Object message, InlineButton inlineButton, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup answer = inlineButton.DocumentsShow(message, baseContext);
			await BotClient.EditMessageTextAsync(_message.From.Id, _message.Message.MessageId, "Выберете статью: ", replyMarkup: answer);
		}

		public async void ChangeFirst(TelegramBotClient BotClient, Message _message, InlineButton inlineButton, User users, DataBaseContext baseContext)
		{
			foreach(BDDocuments temp in baseContext._Documets.Include("user"))
			{
				if(temp.user.ID == _message.From.Id && temp.Work == true)
				{
					temp.Name = _message.Text;

					await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nНовый заголовок: " + temp.Name, replyMarkup: inlineButton.Documents);
					temp.user.Work = 1515;
					temp.Work = false;

				}
			}
		}

		public async void ChangeSecond(TelegramBotClient BotClient, Message _message, InlineButton inlineButton, User users, DataBaseContext baseContext)
		{
			foreach(BDDocuments temp in baseContext._Documets.Include("user"))
			{
				if(temp.user.ID == _message.From.Id && temp.Work == true)
				{
					temp.link = _message.Text;

					await BotClient.SendTextMessageAsync(_message.Chat.Id, "\nНовое содержание: " + temp.link, replyMarkup: inlineButton.Documents);
					temp.user.Work = 1515;
					temp.Work = false;
				}
			}
			baseContext.SaveChanges();
		}

		public void ChangeFirstCat(System.Object message, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			foreach(BDDocuments temp in baseContext._Documets.Include("user"))
			{
				if(temp.user.ID == _message.From.Id && temp.Work == true)
				{
					temp.user.Work = 2415;
				}
			}
		}

		public void ChangeSecondCat(System.Object message, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;

			foreach(BDDocuments temp in baseContext._Documets.Include("user"))
			{
				if(temp.user.ID == _message.From.Id && temp.Work == true)
				{
					temp.user.Work = 2415;
				}
			}
		}

		public void SetNull(System.Object message, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			foreach(BDDocuments temp in baseContext._Documets.Include("user"))
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
