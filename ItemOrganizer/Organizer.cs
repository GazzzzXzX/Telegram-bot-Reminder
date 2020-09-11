using Calendar.OrganizerTelegram;
using Telegram.Bot;
using Telegram.Bot.Types;
using Calendar.Timer;

namespace Calendar.ItemOrganizer
{
	class Organizer : IItemOrganizer
	{
		public async void SetTimeRelaxation(TelegramBotClient BotClient, System.Object message, InlineButtonOrganizer inlineButton, User users, DataBaseContext baseContext)
		{
			Message _message = message as Message;
			System.TimeSpan timeSpan = System.TimeSpan.Parse(_message.Text);
			users.TimeToRelaxation = timeSpan;
			baseContext.SaveChanges();
			await BotClient.SendTextMessageAsync
					(
					_message.Chat.Id,
					"Время работы: " + users.TimeToWork + "\nВремя отдыха: " + _message.Text,
					replyMarkup: inlineButton.StartMenu);
		}
		public async void SetTimeWork(TelegramBotClient BotClient, System.Object message, InlineButtonOrganizer inlineButton, User users, DataBaseContext baseContext)
		{
			Message _message = message as Message;
			System.TimeSpan timeSpan = System.TimeSpan.Parse(_message.Text);
			users.TimeToWork = timeSpan;
			baseContext.SaveChanges();
			await BotClient.SendTextMessageAsync
					(
					_message.Chat.Id,
					"Время работы: " + _message.Text + "\nВремя отдыха: " + users.TimeToRelaxation,
					replyMarkup: inlineButton.StartMenu);
		}
		public void ShowTime(TelegramBotClient BotClient, System.Object message, InlineButtonOrganizer inlineButton, User users, DataBaseContext baseContext)
		{

		}
		public void StartWork(TelegramBotClient BotClient, System.Object message, InlineButtonOrganizer inlineButton, User users, DataBaseContext baseContext)
		{
			System.Console.WriteLine("Timer Start");
			MyTimer myTimer = new MyTimer();
			myTimer.Interval = (users.TimeToWork.Hours * 120 + users.TimeToWork.Minutes * 60 + users.TimeToWork.Seconds) * 1000;
			myTimer.StartWork(BotClient, message, inlineButton, users, baseContext);
		}
		public void StartRelax(TelegramBotClient BotClient, System.Object message, InlineButtonOrganizer inlineButton, User users, DataBaseContext baseContext)
		{
			System.Console.WriteLine("Timer Start");
			MyTimer myTimer = new MyTimer();
			myTimer.Interval = (users.TimeToRelaxation.Hours * 120 + users.TimeToRelaxation.Minutes * 60 + users.TimeToRelaxation.Seconds) * 1000;
			myTimer.StartRelax(BotClient, message, inlineButton, users, baseContext);
		}
	}
}
