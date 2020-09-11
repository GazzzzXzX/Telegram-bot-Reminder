using System.Collections.Generic;
using System.Linq;
using Calendar.Analyzer;
using Calendar.Command;
using Calendar.ItemCalendar;
using Calendar.ItemCategory;
using Calendar.ItemOrganizer;
using Calendar.ItemSettings;
using Calendar.OrganizerTelegram;
using Calendar.Settings;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Calendar
{
	class BotEssence
	{
		protected System.String ApiKey { get; set; }
		protected TelegramBotClient BotClient { get; set; }
		protected DataBaseContext baseContext { get; set; } = new DataBaseContext();

		protected List<ICommand> commands = new List<ICommand>();
		
		protected List<ICommandSettings> commandSettings = new List<ICommandSettings>();
		protected List<IOrganizer> commandiOrganizer = new List<IOrganizer>();
		protected Dictionary<System.Int32, IItemCategory> pairs = new Dictionary<System.Int32, IItemCategory>();
		protected Dictionary<System.Int32, IEventCal> eventCals = new Dictionary<System.Int32, IEventCal>();
		protected Dictionary<System.Int32, Calendar> keyValues = new Dictionary<System.Int32, Calendar>();
		protected Dictionary<System.Int32, IEvetnEach> eventEach = new Dictionary<System.Int32, IEvetnEach>();
		protected Dictionary<System.Int32, ISettings> iSettings = new Dictionary<System.Int32, ISettings>();
		protected Dictionary<System.Int32, IItemOrganizer> iOrganizer = new Dictionary<System.Int32, IItemOrganizer>();

		public BotEssence(System.String Api)
		{
			ApiKey = Api;
			BotClient = new TelegramBotClient(ApiKey);

			SetCalendar();
			SetCommands();
			SetCommandSettings();
			SetIsettings();
			SetCommandiOrganizer();
			setIOrganizer();
			SetEventCals();
			SetEventEach();
			SetCategory();
		}

		private void SetCalendar()
		{
			
		}
		private void SetCommands()
		{
			commands = new List<ICommand>() //--- Список команд
			{
				new StartMenu(),
				new Category(),
				new News(),
				new Sport(),
				new Notes(),
				new Cooking(),
				new Documents(),
				new Video(),
				new Entertainment(),
				new Finance(),
				new Travels(),
				new Cars(),
				new Buy(),
				new Anather(),
				new BackToCategory(),
				new NewsShow(),
				new AddItemCategory(),
				new DeleteNotes(),
				new ChangeFirst(),
				new ChangeSecond(),
				new BackToNews(),
				new StartSettingsOne(),
				new StartOrganizer()

			};
		}
		private void SetCommandSettings()
		{
			commandSettings = new List<ICommandSettings>()
			{
				new AddE_Mail(),
				new BackToSettingsOne(),
				new BackToSettingsTwo(),
				new BackToSettingsThree(),
				new BackToSettingsFour(),
				new DeleteE_Mail(),
				new YesDeleteE_Mail(),
				new NoDeleteE_Mail(),
				new SubscribeToTheNewsletterE_Mail(),
				new SubscribeToTheNewsletterTelegram()
			};
		}
		private void SetIsettings()
		{
			iSettings = new Dictionary<System.Int32, ISettings>()
			{
				{ 37, new E_mail() }
			};
		}
		private void SetCommandiOrganizer()
		{
			commandiOrganizer = new List<IOrganizer>()
			{
				new BackToOrganizer(),
				new OrganizerTimeToWork(),
				new OrganizerTimeToRelaxation(),
				new OrganizerSattisticMouth(),
				new StartOrganizerTime(),
				new StartOrganizerTimeRelax()
			};
		}
		private void setIOrganizer()
		{
			iOrganizer = new Dictionary<System.Int32, IItemOrganizer>()
			{
				{ 38, new Organizer() }
			};
		}
		private void SetEventCals()
		{
			eventCals = new Dictionary<System.Int32, IEventCal>()
			{
				{ 26, new ReminderCal() }, //--- Напоминание
				{ 27, new PurposeCal() }, //--- Цель
				{ 28, new EventCal() }
			};
		}
		private void SetEventEach()
		{
			eventEach = new Dictionary<System.Int32, IEvetnEach>()
			{
				{ 31, new AddEachDay() }
			};
		}
		private void SetCategory()
		{
			pairs = new Dictionary<System.Int32, IItemCategory>()
			{
				{ 0, new NothingCat() },
				{ 11, new NewsCat() },
				{ 12, new SportCat() },
				{ 13, new NotesCat() },
				{ 14, new CookingCat() },
				{ 15, new DocumentsCat() },
				{ 16, new VideoCat() },
				{ 17, new EntertainmentCat() },
				{ 18, new FinanceCat() },
				{ 19, new TravelsCat() },
				{ 20, new CarsCat() },
				{ 21, new BuyCat() },
				{ 22, new AnatherCat() }
			};
		}
		
	}

	class Bot : BotEssence
	{
		public readonly List<ICalendar> calendars = new List<ICalendar>();
		public Bot(System.String api) : base(api)
		{
			System.Console.WriteLine(BotClient.GetMeAsync().Result);

			calendars = new List<ICalendar>()
			{
				#region 1
				new CalendarBot(),
				new Next(),
				new Back(),
				new BackToStart(),
				new BackToCalendar(),
				new AddReminder(),
				new ShowOneDayCalendar(),
				new AddContextReminder(),
				new AddContextPurpose(),
				new AddContextEvent(),
				new BackToMenuReminder(),
				new AddTime(),
				new AddReminderReiteration(),
				new EachDayReminder(),
				new EachWeakReminder(),
				new EachMouthReminder(),
				new EachYearReminder(),
				new EachDayPurpose(),
				new EachWeakPurpose(),
				new EachMouthPurpose(),
				new EachYearPurpose(),
				new EachDayEvent(),
				new EachWeakEvent(),
				new EachMouthEvent(),
				new EachYearEvent(),
				new BackToevent(),
				new AddPurpose(),
				new AddPurooseReiteration(),
				new AddEventReiteration(),
				new DurationPurpose(),
				new BackToPurpose(),
				new BackToReminder(),
				new DurationPurposeTime15(),
				new DurationPurposeTime30(),
				new DurationPurposeTime1(),
				new DurationPurposeTime2(),
				new AddEventNotBusy(),
				new AddEventBusy(),
				new AddEventNotBusy2(),
				new AddLocation(),
				new ShowEvent(),
				new ShowReminder(),
				new ShowPurpose(),
				new BackToChoseDate(),
				new ChangeReminderName(),
				new ChangeReminderNote(),
				new ChangeReminderReiteration(),
				new DeleteAllEvent(),
				new ChangeAllTime(),
#endregion
				new ChangePurposeName(),
				new ChangePurposeNote(),
				new ChangePurposeReiteration(),
				new ChangeEventLocation(),
				new ChangeEventReiteration(),
				new BackToChangePurpose(),
				new BackToChangeReminder(),
				new BackToChangeEvent(),
				new OrganizerSattisticDay(),
				new ChangeEventName(),
				new ChangeEventNote()

			};

			BotClient.OnMessage += BotAnaliz;
			BotClient.OnCallbackQuery += CallBack;

			BotClient.StartReceiving();
		}

		private void CallBack(System.Object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
		{
			if(commands.Any(c => c.Equals(e.CallbackQuery.Data))) // проверка есть ли команда в списке
			{
				ICommand Command = commands.FirstOrDefault(c => c.Equals(e.CallbackQuery.Data)); // вытягиваем класс
				Command.Execute(BotClient, e.CallbackQuery, pairs, baseContext);
			}
			else if(calendars.Any(c => c.Equals(e.CallbackQuery.Data)))
			{
				if(!keyValues.Any(c => c.Key == e.CallbackQuery.From.Id) || keyValues.Count == 0)
				{
					keyValues.Add(e.CallbackQuery.From.Id, new Calendar());
				}

				ICalendar Command = calendars.FirstOrDefault(c => c.Equals(e.CallbackQuery.Data)); // вытягиваем класс
				if(Command.Name == "BackToStart" || Command.Name == "BackToCalendar"
					|| Command.Name == "Calendar" || Command.Name == "<"
					|| Command.Name == ">")
				{
					Command.Execute(BotClient, e.CallbackQuery, keyValues, baseContext);
				}
				else
				{
					Command.Execute(BotClient, e.CallbackQuery, eventCals, baseContext);
				}
			}
			else if(commandSettings.Any(c => c.Equals(e.CallbackQuery.Data)))
			{
				ICommandSettings ISettings = commandSettings.FirstOrDefault(c => c.Equals(e.CallbackQuery.Data));
				ISettings.Execute(BotClient, e.CallbackQuery, iSettings, baseContext);
			}
			else if(commandiOrganizer.Any(c => c.Equals(e.CallbackQuery.Data)))
			{
				IOrganizer iTOrganizer = commandiOrganizer.FirstOrDefault(c => c.Equals(e.CallbackQuery.Data));
				iTOrganizer.Execute(BotClient, e.CallbackQuery, iOrganizer, baseContext);
			}
			else
			{
				User users = baseContext._User.Where(p => p.ID == e.CallbackQuery.From.Id).FirstOrDefault();
				if(users.Work / 100 <= 22)
				{
					ChouseItem chouseItem = new ChouseItem();
					chouseItem.Execute(BotClient, e.CallbackQuery, pairs, baseContext);
				}
				else if(users.Work / 100 == 30)
				{
					ChouseData chouseData = new ChouseData();
					chouseData.Execute(BotClient, e.CallbackQuery, baseContext);
				}
				else if(users.Work % 100 >= 26 && users.Work % 100 <= 28)
				{
					ShowItemCalndar showItemCalnder = new ShowItemCalndar();
					showItemCalnder.Execute(BotClient, e.CallbackQuery, eventCals, baseContext);
				}
			}

		}

		private void BotAnaliz(System.Object sender, Telegram.Bot.Args.MessageEventArgs e)
		{
			if(e.Message.Text != null)
			{
				if(!e.Message.Text.Equals("/start"))
				{
					Analize t = new Analize();

					User users = baseContext._User.Where(p => p.ID == e.Message.From.Id).FirstOrDefault();

					if((users.Work / 100) < 26)
					{
						t.StartAnalize(BotClient, e.Message, pairs, baseContext);
					}
					else if((users.Work / 100) >= 26 && (users.Work / 100) < 31 || (users.Work / 100) == 33
						|| (users.Work / 100) == 34 || (users.Work / 100) == 35 || (users.Work / 100) == 36
						|| (users.Work / 100) == 32)
					{
						t.StartAnalize(BotClient, e.Message, eventCals, baseContext);
					}
					else if((users.Work / 100) == 31)
					{
						t.StartAnalize(BotClient, e.Message, eventEach, baseContext);
					}
					else if((users.Work / 100) == 37)
					{
						t.StartAnalize(BotClient, e.Message, iSettings, baseContext);
					}
					else if((users.Work / 100) == 38 || (users.Work / 100) == 39)
					{
						t.StartAnalize(BotClient, e.Message, iOrganizer, baseContext);
					}
				}
			}
			if(commands.Any(c => c.Equals(e.Message.Text))) // проверка есть ли команда в списке
			{
				try
				{
					commands.FirstOrDefault(c => c.Equals(e.Message.Text)).Execute(BotClient, e.Message, pairs, baseContext); // вытягиваем класс
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine(ex.Message);
				}

			}
			else if(calendars.Any(c => c.Equals(e.Message.Text)))
			{
				try
				{
					if(!keyValues.Any(c => c.Key == e.Message.From.Id) || keyValues.Count == 0)
					{
						keyValues.Add(e.Message.From.Id, new Calendar());
					}

					calendars.FirstOrDefault(c => c.Equals(e.Message.Text)).Execute(BotClient, e.Message, keyValues, baseContext); // вытягиваем класс
				}
				catch(System.Exception ex)
				{
					System.Console.WriteLine(ex.Message);
				}

			}
		}
	}

	class Analize
	{
		private readonly List<List<System.Int32>> Vs = new List<List<System.Int32>>();
		private readonly Dictionary<List<System.Int32>, IAnalize> AbstractAnaliz = new Dictionary<List<System.Int32>, IAnalize>();

		public Analize()
		{
			Vs = new List<List<System.Int32>>
			{
				new List<System.Int32> { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, /*-Для Категории-*/ 26, 27, 28/*-Для календаря-*/ }, //--- Определение категории
				new List<System.Int32> { 23, /*-Для Категории-*/ 29 /*-Для календаря-*/ },	//--- Определение описания
				new List<System.Int32> { 24 },	//--- Определение изменения названия
				new List<System.Int32> { 25 },	//--- Определение изменения описания
				new List<System.Int32> { 30 }, //--- Добавление даты
				new List<System.Int32> { 31 }, //--- Анализ повторение которое будет(Один день, каждый день, каждую неделю, каждый месяц, каждый год)
				new List<System.Int32> { 32 }, //--- Добавление локации.
				new List<System.Int32> { 33 }, //--- Изменение название в календаре
				new List<System.Int32> { 34 }, //--- Изменение описание в календаре
				new List<System.Int32> { 35 }, //--- Изменение времени в календаре
				new List<System.Int32> { 36 },
				new List<System.Int32> { 37 }, //--- Добавление E-Mail
				new List<System.Int32> { 38, 39 }  //--- Добавление TimeToWork and TimeToRelaxation
			};

			AbstractAnaliz = new Dictionary<List<System.Int32>, IAnalize>()
			{
				{ Vs[0], new AnalizeName()          },
				{ Vs[1], new AnalizeContext()       },
				{ Vs[2], new AnalizeChangeFirst()   },
				{ Vs[3], new AnalizeChangeSecond()  },
				{ Vs[4], new AnalizeTime()          },
				{ Vs[5], new AnalizeEach()          },
				{ Vs[6], new AnalizeLocation()      },
				{ Vs[7], new AnalizeChangeFirst()   },
				{ Vs[8], new AnalizeChangeSecond()  },
				{ Vs[9], new AnalizChangeTime()     },
				{ Vs[10], new AnalizChangeLocation()},
				{ Vs[11], new AnalizeE_Mail()		},
				{ Vs[12], new AnalizeTimeOrganizer()}
			};
		}

		public void StartAnalize(TelegramBotClient BotClient, Message _message, System.Object itemCategories, DataBaseContext baseContext)
		{
			InlineButton inlineButton = new InlineButton();
			InlineButtonCalendar inlineButtonCalendar = new InlineButtonCalendar();
			InlineButtonSettings inlineButtonSettings = new InlineButtonSettings();
			InlineButtonOrganizer inlineButtonOrganizer = new InlineButtonOrganizer();

			User users = baseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();

			System.Console.WriteLine(_message.From.Id);
			System.Console.WriteLine(users.Work / 100 + " " + users.Work);


			if((users.Work / 100) < 29 && (users.Work % 100) != 0)
			{
				foreach(KeyValuePair<List<System.Int32>, IAnalize> pair in AbstractAnaliz)
				{
					if(pair.Key.Contains(users.Work / 100) && (users.Work / 100) < 26)
					{
						IAnalize temp = AbstractAnaliz[pair.Key] as IAnalize;
						temp.SetName(BotClient, _message, users, inlineButton, itemCategories, baseContext);
						return;
					}
					else if(pair.Key.Contains(users.Work / 100) && ((users.Work / 100) > 26 || (users.Work / 100) < 29))
					{
						IAnalize temp = AbstractAnaliz[pair.Key] as IAnalize;
						temp.SetName(BotClient, _message, users, inlineButtonCalendar, itemCategories, baseContext);
						return;
					}
				}
			}
			else if((users.Work / 100) >= 29 && (users.Work / 100) < 37 && (users.Work % 100) != 0)
			{
				foreach(KeyValuePair<List<System.Int32>, IAnalize> pair in AbstractAnaliz)
				{
					if(pair.Key.Contains(users.Work / 100))
					{
						IAnalize temp = AbstractAnaliz[pair.Key] as IAnalize;
						temp.SetName(BotClient, _message, users, inlineButtonCalendar, itemCategories, baseContext);
						return;
					}
				}
			}
			else if((users.Work / 100) == 37)
			{
				foreach(KeyValuePair<List<System.Int32>, IAnalize> pair in AbstractAnaliz)
				{
					if(pair.Key.Contains(users.Work / 100))
					{
						IAnalize temp = AbstractAnaliz[pair.Key] as IAnalize;
						temp.SetName(BotClient, _message, users, inlineButtonSettings, itemCategories, baseContext);
					}
				}
			}
			else if((users.Work / 100) == 38 || (users.Work / 100) == 39)
			{
				foreach(KeyValuePair<List<System.Int32>, IAnalize> pair in AbstractAnaliz)
				{
					if(pair.Key.Contains(users.Work / 100))
					{
						IAnalize temp = AbstractAnaliz[pair.Key] as IAnalize;
						temp.SetName(BotClient, _message, users, inlineButtonOrganizer, itemCategories, baseContext);
					}
				}
			}
		}
	}
}
