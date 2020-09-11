using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Calendar.SQL;
using System.Linq;

namespace Calendar.Command
{
	class InlineButtonCalendar
	{
		#region Взаимодействие с календарем
		private List<List<InlineKeyboardButton>> AddNameDay(List<List<InlineKeyboardButton>> list)
		{
			list[list.Count - 1].Add(new InlineKeyboardButton() { Text = "Пн", CallbackData = CommandText.BackToStart });
			list[list.Count - 1].Add(new InlineKeyboardButton() { Text = "Вт", CallbackData = CommandText.BackToStart });
			list[list.Count - 1].Add(new InlineKeyboardButton() { Text = "Ср", CallbackData = CommandText.BackToStart });
			list[list.Count - 1].Add(new InlineKeyboardButton() { Text = "Чт", CallbackData = CommandText.BackToStart });
			list[list.Count - 1].Add(new InlineKeyboardButton() { Text = "Пт", CallbackData = CommandText.BackToStart });
			list[list.Count - 1].Add(new InlineKeyboardButton() { Text = "Сб", CallbackData = CommandText.BackToStart });
			list[list.Count - 1].Add(new InlineKeyboardButton() { Text = "Вс", CallbackData = CommandText.BackToStart });

			return list;
		}

		private List<List<InlineKeyboardButton>> AddNameMouth(List<List<InlineKeyboardButton>> list, Calendar calendar)
		{
			list[list.Count - 1].Add(new InlineKeyboardButton() { Text = calendar.NameCalendar(calendar.date), CallbackData = CommandText.BackToStart });
			return list;
		}

		private List<List<InlineKeyboardButton>> AddBack(List<List<InlineKeyboardButton>> list)
		{
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "<",
				CallbackData = CommandText.Back
			});
			return list;
		}

		private List<List<InlineKeyboardButton>> AddNext(List<List<InlineKeyboardButton>> list)
		{
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = ">",
				CallbackData = CommandText.Next
			});
			return list;
		}

		public InlineKeyboardMarkup CalendarShow(Calendar calendar) //выделение динамических кнопок
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();

			CommandText.MyData.Clear();

			calendar.date = new System.DateTime(calendar.year, calendar.month, 1);
			System.Console.WriteLine(calendar.date);

			list.Add(new List<InlineKeyboardButton>());
			list = AddNameMouth(list, calendar);
			list.Add(new List<InlineKeyboardButton>());
			list = AddNameDay(list);

			calendar.FillCalendar();

			list.Add(new List<InlineKeyboardButton>());
			for(System.Int32 i = 0; i < calendar.calendar.GetLength(0); i++)
			{
				for(System.Int32 j = 0; j < calendar.calendar.GetLength(1); j++)
				{
					if(calendar.calendar[i, j] > 0)
					{
						CommandText.MyData.Add(calendar.calendar[i, j], calendar.calendar[i, j].ToString() + " " + calendar.NameCalendar(calendar.date));
						if(calendar.month == System.DateTime.Today.Month && calendar.calendar[i, j] == System.DateTime.Today.Day && calendar.year == System.DateTime.Today.Year)
						{
							list[list.Count - 1].Add(new InlineKeyboardButton() { Text = "[ " + calendar.calendar[i, j].ToString() + " ]", CallbackData = CommandText.MyData[calendar.calendar[i, j]] });
						}
						else
						{
							list[list.Count - 1].Add(new InlineKeyboardButton() { Text = calendar.calendar[i, j].ToString(), CallbackData = CommandText.MyData[calendar.calendar[i, j]] });
						}

					}
					else
					{
						list[list.Count - 1].Add(new InlineKeyboardButton() { Text = " ", CallbackData = CommandText.BackToStart });
					}
				}
				list.Add(new List<InlineKeyboardButton>());
			}

			list.Add(new List<InlineKeyboardButton>());
			list = AddBack(list);
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Назад",
				CallbackData = CommandText.BackToStart
			});
			list = AddNext(list);

			return new InlineKeyboardMarkup(list);
		}
		#endregion

		#region Повторение
		public InlineKeyboardMarkup PurposeShow = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Не повторять", CallbackData = CommandText.BackToEvent},
				new InlineKeyboardButton { Text = "Каждый день", CallbackData = CommandText.EachDayPurpose}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Каждую неделю", CallbackData = CommandText.EachWeakPurpose},
				new InlineKeyboardButton { Text = "Каждый месяц", CallbackData = CommandText.EachMouthPurpose}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Каждый год", CallbackData = CommandText.EachYearPurpose},
				new InlineKeyboardButton { Text = "Назад", CallbackData = CommandText.BackToPurpose}
			}

		};

		public InlineKeyboardMarkup ReminderShow = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Не повторять", CallbackData = CommandText.BackToEvent},
				new InlineKeyboardButton { Text = "Каждый день", CallbackData = CommandText.EachDayReminder}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Каждую неделю", CallbackData = CommandText.EachWeakReminder},
				new InlineKeyboardButton { Text = "Каждый месяц", CallbackData = CommandText.EachMouthReminder}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Каждый год", CallbackData = CommandText.EachYearReminder},
				new InlineKeyboardButton { Text = "Назад", CallbackData = CommandText.BackToReminder}
			}

		};

		public InlineKeyboardMarkup EventShow = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Не повторять", CallbackData = CommandText.BackToEvent},
				new InlineKeyboardButton { Text = "Каждый день", CallbackData = CommandText.EachDayEvent}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Каждую неделю", CallbackData = CommandText.EachWeakEvent},
				new InlineKeyboardButton { Text = "Каждый месяц", CallbackData = CommandText.EachMouthEvent}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Каждый год", CallbackData = CommandText.EachYearEvent},
				new InlineKeyboardButton { Text = "Назад", CallbackData = CommandText.BackToEvent}
			}

		};

		public InlineKeyboardMarkup DurationPurpose = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "15 минут", CallbackData = CommandText.DurationPurposeTime15 },
				new InlineKeyboardButton{ Text = "30 минут", CallbackData = CommandText.DurationPurposeTime30 }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "1 час", CallbackData = CommandText.DurationPurposeTime1 },
				new InlineKeyboardButton{ Text = "2 часа", CallbackData = CommandText.DurationPurposeTime2 }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToPurpose }
			}

		};
		#endregion

		#region Добавление
		public InlineKeyboardMarkup AddReminder = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить время", CallbackData = CommandText.AddTime},
				new InlineKeyboardButton { Text = "Добавить описание", CallbackData = CommandText.AddContextReminder}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Повторять", CallbackData = CommandText.AddReminderReiteration},
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToMenuReminder }
			}
		};

		public InlineKeyboardMarkup AddPurpose = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить время", CallbackData = CommandText.AddTime},
				new InlineKeyboardButton { Text = "Добавить описание", CallbackData = CommandText.AddContextPurpose}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Повторять", CallbackData = CommandText.AddPurooseReiteration},
				new InlineKeyboardButton{ Text = "Продолжительность", CallbackData = CommandText.DurationPurpose }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToMenuReminder }
			}
		};

		public InlineKeyboardMarkup AddEventBusy = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить время", CallbackData = CommandText.AddTime}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить описание", CallbackData = CommandText.AddContextEvent},
				new InlineKeyboardButton { Text = "Повторять", CallbackData = CommandText.AddEventReiteration},
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Место проведения", CallbackData = CommandText.AddLocation },
				new InlineKeyboardButton { Text = "Занят(а)", CallbackData = CommandText.AddEventNotBusy2},
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToMenuReminder }
			}
		};

		public InlineKeyboardMarkup AddEventNotBusy = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить время", CallbackData = CommandText.AddTime}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить описание", CallbackData = CommandText.AddContextEvent},
				new InlineKeyboardButton { Text = "Повторять", CallbackData = CommandText.AddEventReiteration},
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Место проведения", CallbackData = CommandText.AddLocation },
				new InlineKeyboardButton { Text = "Не занят(а)", CallbackData = CommandText.AddEventBusy},
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToMenuReminder }
			}
		};
		#endregion

		#region Демонстрация событий в каледаре
		public InlineKeyboardMarkup ShowCategoryCalendar(System.Object message, DataBaseContext baseContext)
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;

			TempDate tempDate = baseContext._TempDate.Where(p => p.user.ID == mes.From.Id).FirstOrDefault();

			System.Boolean isEvent = false;
			System.Boolean isPurpose = false;
			System.Boolean isReminder = false;

			foreach(EventTime eventTime in baseContext._EventTimes)
			{
				if(eventTime.dateTime.Date == tempDate.date.Date)
				{
					if(isEvent == false)
					{
						foreach(Event _event in baseContext._Events.Include("user"))
						{
							if(_event.user.ID == mes.From.Id)
							{
								if(eventTime._event != null)
								{
									if(_event.ID == eventTime._event.ID)
									{
										list.Add(new List<InlineKeyboardButton>());
										list[list.Count - 1].Add(new InlineKeyboardButton() { Text = "Мероприятие", CallbackData = CommandText.ShowEvent });
										isEvent = true;
										break;
									}
								}
							}
						}
					}
					if(isPurpose == false)
					{
						foreach(Purpose purpose in baseContext._Purposes.Include("user"))
						{
							if(purpose.user.ID == mes.From.Id)
							{
								if(eventTime.purpose != null)
								{
									if(purpose.ID == eventTime.purpose.ID)
									{
										list.Add(new List<InlineKeyboardButton>());
										list[list.Count - 1].Add(new InlineKeyboardButton() { Text = "Цель", CallbackData = CommandText.ShowPurpose });
										isPurpose = true;
										break;
									}
								}
							}
						}
					}
					if(isReminder == false)
					{
						foreach(Reminder reminder in baseContext._Reminders.Include("user"))
						{
							if(reminder.user.ID == mes.From.Id)
							{
								if(eventTime.reminder != null)
								{
									if(reminder.ID == eventTime.reminder.ID)
									{
										list.Add(new List<InlineKeyboardButton>());
										list[list.Count - 1].Add(new InlineKeyboardButton() { Text = "Напоминание", CallbackData = CommandText.ShowReminder });
										isReminder = true;
										break;
									}
								}
							}
						}
					}
				}

			}
			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton() { Text = "Назад", CallbackData = CommandText.BackToCalendar });
			return new InlineKeyboardMarkup(list);
		}

		public InlineKeyboardMarkup ShowEvent(System.Object message, DataBaseContext baseContext)
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;

			CommandText.ChouseAllEvent.Clear();

			System.Int32 i = 0;

			list.Add(new List<InlineKeyboardButton>());
			foreach(Event _event in baseContext._Events.Include("user"))
			{
				if(_event.user.ID == mes.From.Id)
				{
					foreach(EventTime eventTime in baseContext._EventTimes.Include("_event"))
					{
						if(eventTime._event != null)
						{
							if(eventTime._event.ID == _event.ID)
							{
								TempDate tempDate = baseContext._TempDate.Where(p => p.user == _event.user).FirstOrDefault();

								try
								{
									if(tempDate.date.Date == eventTime.dateTime.Date)
									{
										if(i >= 2)
										{
											list.Add(new List<InlineKeyboardButton>());
										}
										CommandText.ChouseAllEvent.Add(_event.ID, _event.Name.ToString());
										list[list.Count - 1].Add(new InlineKeyboardButton()
										{
											Text = _event.Name,
											CallbackData = _event.ID + " " + CommandText.ChouseAllEvent[_event.ID]
										});
										i++;
									}
								}
								catch
								{
									System.Console.WriteLine("public InlineKeyboardMarkup ShowEvent(System.Object message)");
								}
							}
						}
					}
				}
			}
			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton() { Text = "Назад", CallbackData = CommandText.BackToCalendar });
			return new InlineKeyboardMarkup(list);
		}

		public InlineKeyboardMarkup ShowPurpose(System.Object message, DataBaseContext baseContext)
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;

			CommandText.ChouseAllEvent.Clear();

			System.Int32 i = 0;
			list.Add(new List<InlineKeyboardButton>());
			foreach(Purpose purpose in baseContext._Purposes.Include("user"))
			{
				if(purpose.user.ID == mes.From.Id)
				{
					foreach(EventTime eventTime in baseContext._EventTimes.Include("purpose"))
					{
						if(eventTime.purpose != null)
						{
							if(eventTime.purpose.ID == purpose.ID)
							{
								TempDate tempDate = baseContext._TempDate.Where(p => p.user == purpose.user).FirstOrDefault();

								try
								{
									if(tempDate.date.Date == eventTime.dateTime.Date)
									{
										if(i >= 2)
										{
											list.Add(new List<InlineKeyboardButton>());
										}
										CommandText.ChouseAllEvent.Add(purpose.ID, purpose.Name.ToString());
										list[list.Count - 1].Add(new InlineKeyboardButton()
										{
											Text = purpose.Name,
											CallbackData = purpose.ID + " " + CommandText.ChouseAllEvent[purpose.ID]
										});
										i++;
									}
								}
								catch
								{
									System.Console.WriteLine("public InlineKeyboardMarkup ShowPurpose(System.Object message)");
								}
							}
						}
					}
				}
			}
			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton() { Text = "Назад", CallbackData = CommandText.BackToCalendar });
			return new InlineKeyboardMarkup(list);
		}

		public InlineKeyboardMarkup ShowReminder(System.Object message, DataBaseContext baseContext)
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;

			CommandText.ChouseAllEvent.Clear();

			System.Int32 i = 0;

			list.Add(new List<InlineKeyboardButton>());
			foreach(Reminder reminder in baseContext._Reminders.Include("user"))
			{
				if(reminder.user.ID == mes.From.Id)
				{
					foreach(EventTime eventTime in baseContext._EventTimes.Include("reminder"))
					{
						if(eventTime.reminder != null)
						{
							if(eventTime.reminder.ID == reminder.ID)
							{
								TempDate tempDate = baseContext._TempDate.Where(p => p.user == reminder.user).FirstOrDefault();

								try
								{
									if(tempDate.date.Date == eventTime.dateTime.Date)
									{
										if(i >= 2)
										{
											list.Add(new List<InlineKeyboardButton>());
										}
										CommandText.ChouseAllEvent.Add(reminder.ID, reminder.Name.ToString());
										list[list.Count - 1].Add(new InlineKeyboardButton()
										{
											Text = reminder.Name,
											CallbackData = reminder.ID + " " + CommandText.ChouseAllEvent[reminder.ID]
										});
										i++;
									}
								}
								catch
								{
									System.Console.WriteLine("public InlineKeyboardMarkup ShowReminder(System.Object message)");
								}
							}
						}
					}
				}
			}
			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton() { Text = "Назад", CallbackData = CommandText.BackToCalendar });
			return new InlineKeyboardMarkup(list);
		}
		#endregion

		#region Изменение
		public InlineKeyboardMarkup ChangeReminder = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Изменить время", CallbackData = CommandText.ChangeAllTime},
				new InlineKeyboardButton { Text = "Изменить название", CallbackData = CommandText.ChangeReminderName}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Изменить описание", CallbackData = CommandText.ChangeReminderNote },
				new InlineKeyboardButton { Text = "Изменить повторение", CallbackData = CommandText.ChangeReminderReiteration }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Удалить", CallbackData = CommandText.DeleteAllEvent }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Назад", CallbackData = CommandText.BackToChoseDate }
			}



		};

		public InlineKeyboardMarkup ChangePurpose = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Изменить время", CallbackData = CommandText.ChangeAllTime},
				new InlineKeyboardButton { Text = "Изменить название", CallbackData = CommandText.ChangePurposeName}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Изменить описание", CallbackData = CommandText.ChangePurposeNote },
				new InlineKeyboardButton { Text = "Изменить повторение", CallbackData = CommandText.ChangePurposeReiteration }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Изменить продолжительность", CallbackData = CommandText.DurationPurpose },
				new InlineKeyboardButton { Text = "Удалить", CallbackData = CommandText.DeleteAllEvent }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Назад", CallbackData = CommandText.BackToChoseDate }
			}

		};

		public InlineKeyboardMarkup ChangeEvent = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Изменить время", CallbackData = CommandText.ChangeAllTime},
				new InlineKeyboardButton { Text = "Изменить название", CallbackData = CommandText.ChangeEventName}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Изменить описание", CallbackData = CommandText.ChangeEventNote },
				new InlineKeyboardButton { Text = "Изменить повторение", CallbackData = CommandText.ChangeEventReiteration }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Изменить место проведение", CallbackData = CommandText.ChangeEventLocation },
				new InlineKeyboardButton { Text = "Удалить", CallbackData = CommandText.DeleteAllEvent }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Назад", CallbackData = CommandText.BackToChoseDate }
			}

		};
		#endregion

		public InlineKeyboardMarkup BackToPurpose = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToPurpose }
			}
		};
		public InlineKeyboardMarkup BackToChangePurpose = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToChangePurpose }
			}
		};

		public InlineKeyboardMarkup BackToReminder = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToReminder }
			}
		};
		public InlineKeyboardMarkup BackToChangeReminder = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToChangeReminder }
			}
		};

		public InlineKeyboardMarkup BackToEvent = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToEvent }
			}
		};
		public InlineKeyboardMarkup BackToChangeEvent = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToChangeEvent }
			}
		};


		public InlineKeyboardMarkup ChouseDate = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить напоминание", CallbackData = CommandText.AddReminder},
				new InlineKeyboardButton { Text = "Добавить мероприятие", CallbackData = CommandText.AddEventNotBusy}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить цель", CallbackData = CommandText.AddPurpose},
				new InlineKeyboardButton{ Text = "Посмотреть событие", CallbackData = CommandText.ShowOneDayCalendar }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Статистика за день", CallbackData = CommandText.OrganizerSattisticDay }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToStart }
			}
		};

		public InlineKeyboardMarkup BackToCalendar  = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToStart }
			}
		};
	}
}