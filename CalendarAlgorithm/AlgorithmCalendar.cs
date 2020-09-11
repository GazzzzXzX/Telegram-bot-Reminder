using System.Linq;
using System.Threading;
using Calendar.SQL;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

namespace Calendar.CalendarAlgorithm
{
	class AlgorithmCalendar
	{
		
		TempDate _tempDate;
		EventTime _eventTime;
		readonly Event _event;
		readonly Purpose _purpose;
		readonly Reminder _reminder;
		User _user;
		private DataBaseContext BaseContext { get; set; } = new DataBaseContext();

		public delegate void SetDate(CallbackQuery _message);
		public SetDate mes;
		
		public AlgorithmCalendar(CallbackQuery _message)
		{
			_user = BaseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			_tempDate = BaseContext._TempDate.Where(p => p.user.ID == _message.From.Id).FirstOrDefault();
			_eventTime = null;
			_event = BaseContext._Events.Where(p => p.user == _user).FirstOrDefault();
			_purpose = BaseContext._Purposes.Where(p => p.user == _user).FirstOrDefault();
			_reminder = BaseContext._Reminders.Where(p => p.user == _user).FirstOrDefault();

			SetDelegate();
		}

		private void SetDelegate()
		{
			if(_event != null)
			{
				mes += SetEachDayEventDate;
				mes += SetEachWeakEventDate;
				mes += SetEachMouthEventDate;
				mes += SetEachYearEventDate;
			}
			if(_reminder != null)
			{
				mes += SetEachDayReminderDate;
				mes += SetEachWeakReminderDate;
				mes += SetEachMouthReminderDate;
				mes += SetEachYearReminderDate;
			}
			if(_purpose != null)
			{
				mes += SetEachDayPurposeDate;
				mes += SetEachWeakPurposeDate;
				mes += SetEachMouthPurposeDate;
				mes += SetEachYearPurposeDate;
			}
		}
		private System.Boolean CheckDate(EventTime ev)
		{
			try
			{
				if(_tempDate.date >= ev.dateTime.Date)
				{
					return true;
				}
			}
			catch
			{
				return false;
			}
			return false;
		}
		

		private void SetEachDayEventDate(CallbackQuery _message)
		{
			_user = BaseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			_tempDate = BaseContext._TempDate.Where(p => p.user.ID == _user.ID).FirstOrDefault();

			Thread myThread = new Thread(new ThreadStart(StartThreadEachDayEvent));
			myThread.Start(); // запускаем поток
			myThread.Join();
		}
		private void SetEachWeakEventDate(CallbackQuery _message)
		{
			_user = BaseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			_tempDate = BaseContext._TempDate.Where(p => p.user.ID == _user.ID).FirstOrDefault();

			Thread myThread = new Thread(new ThreadStart(StartThreadEachWeakEvent));
			myThread.Start(); // запускаем поток
			myThread.Join();
		}
		private void SetEachMouthEventDate(CallbackQuery _message)
		{
			_user = BaseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			_tempDate = BaseContext._TempDate.Where(p => p.user.ID == _user.ID).FirstOrDefault();

			Thread myThread = new Thread(new ThreadStart(StartThreadEachMouthEvent));
			myThread.Start(); // запускаем поток
			myThread.Join();
		}
		private void SetEachYearEventDate(CallbackQuery _message)
		{
			_user = BaseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			_tempDate = BaseContext._TempDate.Where(p => p.user.ID == _user.ID).FirstOrDefault();

			Thread myThread = new Thread(new ThreadStart(StartThreadEachYearEvent));
			myThread.Start(); // запускаем поток
			myThread.Join();
		}

		private void SetEachDayReminderDate(CallbackQuery _message)
		{
			_user = BaseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			_tempDate = BaseContext._TempDate.Where(p => p.user.ID == _user.ID).FirstOrDefault();

			Thread myThread = new Thread(new ThreadStart(StartThreadEachDayReminder));
			myThread.Start(); // запускаем поток
			myThread.Join();
		}
		private void SetEachWeakReminderDate(CallbackQuery _message)
		{
			_user = BaseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			_tempDate = BaseContext._TempDate.Where(p => p.user.ID == _user.ID).FirstOrDefault();

			Thread myThread = new Thread(new ThreadStart(StartThreadEachWeakReminder));
			myThread.Start(); // запускаем поток
			myThread.Join();
		}
		private void SetEachMouthReminderDate(CallbackQuery _message)
		{
			_user = BaseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			_tempDate = BaseContext._TempDate.Where(p => p.user.ID == _user.ID).FirstOrDefault();

			Thread myThread = new Thread(new ThreadStart(StartThreadEachMouthReminder));
			myThread.Start(); // запускаем поток
			myThread.Join();
		}
		private void SetEachYearReminderDate(CallbackQuery _message)
		{
			_user = BaseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			_tempDate = BaseContext._TempDate.Where(p => p.user.ID == _user.ID).FirstOrDefault();

			Thread myThread = new Thread(new ThreadStart(StartThreadEachYearReminder));
			myThread.Start(); // запускаем поток
			myThread.Join();
		}

		private void SetEachDayPurposeDate(CallbackQuery _message)
		{
			_user = BaseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			_tempDate = BaseContext._TempDate.Where(p => p.user.ID == _user.ID).FirstOrDefault();

			Thread myThread = new Thread(new ThreadStart(StartThreadEachDayPurpose));
			myThread.Start(); // запускаем поток
			myThread.Join();
		}
		private void SetEachWeakPurposeDate(CallbackQuery _message)
		{
			_user = BaseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			_tempDate = BaseContext._TempDate.Where(p => p.user.ID == _user.ID).FirstOrDefault();

			Thread myThread = new Thread(new ThreadStart(StartThreadEachWeakPurpose));
			myThread.Start(); // запускаем поток
			myThread.Join();
		}
		private void SetEachMouthPurposeDate(CallbackQuery _message)
		{
			_user = BaseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			_tempDate = BaseContext._TempDate.Where(p => p.user.ID == _user.ID).FirstOrDefault();

			Thread myThread = new Thread(new ThreadStart(StartThreadEachMouthPurpose));
			myThread.Start(); // запускаем поток
			myThread.Join();
		}
		private void SetEachYearPurposeDate(CallbackQuery _message)
		{
			_user = BaseContext._User.Where(p => p.ID == _message.From.Id).FirstOrDefault();
			_tempDate = BaseContext._TempDate.Where(p => p.user.ID == _user.ID).FirstOrDefault();

			Thread myThread = new Thread(new ThreadStart(StartThreadEachYearPurpose));
			myThread.Start(); // запускаем поток
			myThread.Join();
		}

		private void StartThreadEachDayEvent()
		{
			foreach(Event ev in BaseContext._Events.Include("user"))
			{
				if(ev.user.ID == _user.ID)
				{
					_eventTime = BaseContext._EventTimes.Where(p => p._event == ev).FirstOrDefault(p => p.DoWork == 1);

					if(_eventTime != null)
					{
						if(CheckDate(_eventTime))
						{
							System.DateTime dt = _tempDate.date;
							EventTime eventTime = BaseContext._EventTimes.Where(p => p._event == ev).FirstOrDefault(p => p.dateTime.DayOfYear == _tempDate.date.DayOfYear);
							if(eventTime == null)
							{
								System.TimeSpan timeSpan = _eventTime.dateTime.TimeOfDay;
								dt += timeSpan;

								BaseContext._Events.Add
									(
										new Event()
										{
											Name = ev.Name,
											Note = ev.Note,
											Location = ev.Location,
											user = _user,
											Busy = ev.Busy,
											Count = ev.Count,
											Work = false
										}
									);
								BaseContext._EventTimes.Add
									(
										new EventTime()
										{
											_event = ev,
											DoWork = _eventTime.DoWork,
											dateTime = dt,
											Work = _eventTime.Work,
										}
									);
							}
							else
							{ break; }
						}
					}
				}
			}
			BaseContext.SaveChanges();
			System.Console.WriteLine("Поток StartThreadEachDayEvent завершен");
		}
		private void StartThreadEachWeakEvent()
		{
			foreach(Event ev in BaseContext._Events.Include("user"))
			{
				if(ev.user.ID == _user.ID)
				{
					_eventTime = BaseContext._EventTimes.Where(p => p._event == ev).FirstOrDefault(p => p.DoWork == 2);

					if(_eventTime != null)
					{
						if(CheckDate(_eventTime))
						{
							if(((_tempDate.date.Date - _eventTime.dateTime.Date).TotalDays % 7) == 0)
							{
								System.DateTime dt = _tempDate.date;

								EventTime eventTime = BaseContext._EventTimes.Where(p => p._event == ev).FirstOrDefault(p => p.dateTime.DayOfYear == _tempDate.date.DayOfYear);
								if(eventTime == null)
								{
									System.TimeSpan timeSpan = _eventTime.dateTime.TimeOfDay;
									dt += timeSpan;

									BaseContext._Events.Add
										(
											new Event()
											{
												Name = ev.Name,
												Note = ev.Note,
												Location = ev.Location,
												user = _user,
												Busy = ev.Busy,
												Count = ev.Count,
												Work = false
											}
										);
									BaseContext._EventTimes.Add
										(
											new EventTime()
											{
												_event = ev,
												DoWork = _eventTime.DoWork,
												dateTime = dt,
												Work = _eventTime.Work,
											}
										);
								}
								else
								{ break; }
							}
						}
					}
				}
			}
			BaseContext.SaveChanges();
			System.Console.WriteLine("Поток StartThreadEachWeakEvent завершен");
		}
		private void StartThreadEachMouthEvent()
		{
			foreach(Event ev in BaseContext._Events.Include("user"))
			{
				if(ev.user.ID == _user.ID)
				{
					_eventTime = BaseContext._EventTimes.Where(p => p._event == ev).FirstOrDefault(p => p.DoWork == 3);

					if(_eventTime != null)
					{
						if(CheckDate(_eventTime))
						{
							if(_eventTime.dateTime.Day == _tempDate.date.Day)
							{
								System.DateTime dt = _tempDate.date;

								EventTime eventTime = BaseContext._EventTimes.Where(p => p._event == ev).FirstOrDefault(p => p.dateTime.DayOfYear == _tempDate.date.DayOfYear);
								if(eventTime == null)
								{
									System.TimeSpan timeSpan = _eventTime.dateTime.TimeOfDay;
									dt += timeSpan;

									BaseContext._Events.Add
										(
											new Event()
											{
												Name = ev.Name,
												Note = ev.Note,
												Location = ev.Location,
												user = _user,
												Busy = ev.Busy,
												Count = ev.Count,
												Work = false
											}
										);
									BaseContext._EventTimes.Add
										(
											new EventTime()
											{
												_event = ev,
												DoWork = _eventTime.DoWork,
												dateTime = dt,
												Work = _eventTime.Work,
											}
										);
								}
								else
								{ break; }
							}
						}
					}
				}
			}
			BaseContext.SaveChanges();
			System.Console.WriteLine("Поток StartThreadEachMouthEvent завершен");
		}
		private void StartThreadEachYearEvent()
		{
			foreach(Event ev in BaseContext._Events.Include("user"))
			{
				if(ev.user.ID == _user.ID)
				{
					_eventTime = BaseContext._EventTimes.Where(p => p._event == ev).FirstOrDefault(p => p.DoWork == 4);

					if(_eventTime != null)
					{
						if(CheckDate(_eventTime))
						{
							if(_eventTime.dateTime.Day == _tempDate.date.Day && _eventTime.dateTime.Month == _tempDate.date.Month)
							{
								System.DateTime dt = _tempDate.date;

								EventTime eventTime = BaseContext._EventTimes.Where(p => p._event == ev).FirstOrDefault(p => p.dateTime.DayOfYear == _tempDate.date.DayOfYear);
								if(eventTime == null)
								{
									System.TimeSpan timeSpan = _eventTime.dateTime.TimeOfDay;
									dt += timeSpan;

									BaseContext._Events.Add
										(
											new Event()
											{
												Name = ev.Name,
												Note = ev.Note,
												Location = ev.Location,
												user = _user,
												Busy = ev.Busy,
												Count = ev.Count,
												Work = false
											}
										);
									BaseContext._EventTimes.Add
										(
											new EventTime()
											{
												_event = ev,
												DoWork = _eventTime.DoWork,
												dateTime = dt,
												Work = _eventTime.Work,
											}
										);
								}
								else
								{ break; }
							}
						}
					}
				}
			}
			BaseContext.SaveChanges();
			System.Console.WriteLine("Поток StartThreadEachYearEvent завершен");
		}

		private void StartThreadEachDayReminder()
		{
			foreach(Reminder ev in BaseContext._Reminders.Include("user"))
			{
				if(ev.user.ID == _user.ID)
				{
					_eventTime = BaseContext._EventTimes.Where(p => p.reminder == ev).FirstOrDefault(p => p.DoWork == 1);

					if(_eventTime != null)
					{
						if(CheckDate(_eventTime))
						{
							System.DateTime dt = _tempDate.date;

							EventTime eventTime = BaseContext._EventTimes.Where(p => p.reminder == ev).FirstOrDefault(p => p.dateTime.DayOfYear == _tempDate.date.DayOfYear);
							if(eventTime == null)
							{
								System.TimeSpan timeSpan = _eventTime.dateTime.TimeOfDay;
								dt += timeSpan;

								BaseContext._Reminders.Add
									(
										new Reminder()
										{
											Name = ev.Name,
											Note = ev.Note,
											user = _user,
											Work = false
										}
									);
								BaseContext._EventTimes.Add
									(
										new EventTime()
										{
											reminder = ev,
											DoWork = _eventTime.DoWork,
											dateTime = dt,
											Work = _eventTime.Work,
										}
									);
							}
							else
							{ break; }
						}
					}
				}
			}
			BaseContext.SaveChanges();
			System.Console.WriteLine("Поток StartThreadEachDayReminder завершен");
		}
		private void StartThreadEachWeakReminder()
		{
			foreach(Reminder ev in BaseContext._Reminders.Include("user"))
			{
				if(ev.user.ID == _user.ID)
				{
					_eventTime = BaseContext._EventTimes.Where(p => p.reminder == ev).FirstOrDefault(p => p.DoWork == 2);

					if(_eventTime != null)
					{
						if(CheckDate(_eventTime))
						{
							if(((_tempDate.date.Date - _eventTime.dateTime.Date).TotalDays % 7) == 0)
							{
								System.DateTime dt = _tempDate.date;

								EventTime eventTime = BaseContext._EventTimes.Where(p => p.reminder == ev).FirstOrDefault(p => p.dateTime.DayOfYear == _tempDate.date.DayOfYear);
								if(eventTime == null)
								{
									System.TimeSpan timeSpan = _eventTime.dateTime.TimeOfDay;
									dt += timeSpan;

									BaseContext._Reminders.Add
										(
											new Reminder()
											{
												Name = ev.Name,
												Note = ev.Note,
												user = _user,
												Work = false
											}
										);
									BaseContext._EventTimes.Add
										(
											new EventTime()
											{
												reminder = ev,
												DoWork = _eventTime.DoWork,
												dateTime = dt,
												Work = _eventTime.Work,
											}
										);
								}
								else
								{ break; }
							}
						}
					}
				}
			}
			BaseContext.SaveChanges();
			System.Console.WriteLine("Поток StartThreadEachWeakReminder завершен");
		}
		private void StartThreadEachMouthReminder()
		{
			foreach(Reminder ev in BaseContext._Reminders.Include("user"))
			{
				_eventTime = BaseContext._EventTimes.Where(p => p.reminder == ev).FirstOrDefault(p => p.DoWork == 3);

				if(_eventTime != null)
				{
					if(_eventTime.dateTime.Day == _tempDate.date.Day)
					{
						if(CheckDate(_eventTime))
						{
							System.DateTime dt = _tempDate.date;

							EventTime eventTime = BaseContext._EventTimes.Where(p => p.reminder == ev).FirstOrDefault(p => p.dateTime.DayOfYear == _tempDate.date.DayOfYear);
							if(eventTime == null)
							{
								System.TimeSpan timeSpan = _eventTime.dateTime.TimeOfDay;
								dt += timeSpan;

								BaseContext._Reminders.Add
									(
										new Reminder()
										{
											Name = ev.Name,
											Note = ev.Note,
											user = _user,
											Work = false
										}
									);
								BaseContext._EventTimes.Add
									(
										new EventTime()
										{
											reminder = ev,
											DoWork = _eventTime.DoWork,
											dateTime = dt,
											Work = _eventTime.Work,
										}
									);
							}
							else
							{ break; }
						}
					}
				}
			}
			BaseContext.SaveChanges();
			System.Console.WriteLine("Поток StartThreadEachMouthReminder завершен");
		}
		private void StartThreadEachYearReminder()
		{
			foreach(Reminder ev in BaseContext._Reminders.Include("user"))
			{
				if(ev.user.ID == _user.ID)
				{
					_eventTime = BaseContext._EventTimes.Where(p => p.reminder == ev).FirstOrDefault(p => p.DoWork == 4);

					if(_eventTime != null)
					{
						if(CheckDate(_eventTime))
						{
							if(_eventTime.dateTime.Day == _tempDate.date.Day && _eventTime.dateTime.Month == _tempDate.date.Month)
							{
								System.DateTime dt = _tempDate.date;

								EventTime eventTime = BaseContext._EventTimes.Where(p => p.reminder == ev).FirstOrDefault(p => p.dateTime.DayOfYear == _tempDate.date.DayOfYear);
								if(eventTime == null)
								{
									System.TimeSpan timeSpan = _eventTime.dateTime.TimeOfDay;
									dt += timeSpan;

									BaseContext._Reminders.Add
										(
											new Reminder()
											{
												Name = ev.Name,
												Note = ev.Note,
												user = _user,
												Work = false
											}
										);
									BaseContext._EventTimes.Add
										(
											new EventTime()
											{
												reminder = ev,
												DoWork = _eventTime.DoWork,
												dateTime = dt,
												Work = _eventTime.Work,
											}
										);
								}
								else
								{ break; }
							}
						}
					}
				}
			}
			BaseContext.SaveChanges();
			System.Console.WriteLine("Поток StartThreadEachYearReminder завершен");
		}

		private void StartThreadEachDayPurpose()
		{
			foreach(Purpose ev in BaseContext._Purposes.Include("user"))
			{
				if(ev.user.ID == _user.ID)
				{
					_eventTime = BaseContext._EventTimes.Where(p => p.purpose == ev).FirstOrDefault(p => p.DoWork == 1);

					if(_eventTime != null)
					{
						if(CheckDate(_eventTime))
						{
							System.DateTime dt = _tempDate.date;

							EventTime eventTime = BaseContext._EventTimes.Where(p => p.purpose == ev).FirstOrDefault(p => p.dateTime.DayOfYear == _tempDate.date.DayOfYear);
							if(eventTime == null)
							{
								System.TimeSpan timeSpan = _eventTime.dateTime.TimeOfDay;
								dt += timeSpan;

								BaseContext._Purposes.Add
									(
										new Purpose()
										{
											Name = ev.Name,
											Note = ev.Note,
											user = _user,
											Duration = ev.Duration,
											IDFrequency = ev.IDFrequency,
											Work = false
										}
									);
								BaseContext._EventTimes.Add
									(
										new EventTime()
										{
											purpose = ev,
											DoWork = _eventTime.DoWork,
											dateTime = dt,
											Work = _eventTime.Work,
										}
									);
							}
							else
							{ break; }
						}
					}
				}
			}
			BaseContext.SaveChanges();
			System.Console.WriteLine("Поток StartThreadEachDayPurpose завершен");
		}
		private void StartThreadEachWeakPurpose()
		{
			foreach(Purpose ev in BaseContext._Purposes.Include("user"))
			{
				if(ev.user.ID == _user.ID)
				{
					_eventTime = BaseContext._EventTimes.Where(p => p.purpose == ev).FirstOrDefault(p => p.DoWork == 2);

					if(_eventTime != null)
					{
						if(CheckDate(_eventTime))
						{
							if(((_tempDate.date.Date - _eventTime.dateTime.Date).TotalDays % 7) == 0)
							{
								System.DateTime dt = _tempDate.date;

								EventTime eventTime = BaseContext._EventTimes.Where(p => p.purpose == ev).FirstOrDefault(p => p.dateTime.DayOfYear == _tempDate.date.DayOfYear);
								if(eventTime == null)
								{
									System.TimeSpan timeSpan = _eventTime.dateTime.TimeOfDay;
									dt += timeSpan;

									BaseContext._Purposes.Add
										(
											new Purpose()
											{
												Name = ev.Name,
												Note = ev.Note,
												user = _user,
												Duration = ev.Duration,
												IDFrequency = ev.IDFrequency,
												Work = false
											}
										);
									BaseContext._EventTimes.Add
										(
											new EventTime()
											{
												purpose = ev,
												DoWork = _eventTime.DoWork,
												dateTime = dt,
												Work = _eventTime.Work,
											}
										);
								}
								else
								{ break; }
							}
						}
					}
				}
			}
			BaseContext.SaveChanges();
			System.Console.WriteLine("Поток StartThreadEachWeakPurpose завершен");
		}
		private void StartThreadEachMouthPurpose()
		{
			foreach(Purpose ev in BaseContext._Purposes.Include("user"))
			{
				if(ev.user.ID == _user.ID)
				{
					_eventTime = BaseContext._EventTimes.Where(p => p.purpose == ev).FirstOrDefault(p => p.DoWork == 3);

					if(_eventTime != null)
					{
						if(CheckDate(_eventTime))
						{
							if(_eventTime.dateTime.Day == _tempDate.date.Day)
							{
								System.DateTime dt = _tempDate.date;

								EventTime eventTime = BaseContext._EventTimes.Where(p => p.purpose == ev).FirstOrDefault(p => p.dateTime.DayOfYear == _tempDate.date.DayOfYear);
								if(eventTime == null)
								{
									System.TimeSpan timeSpan = _eventTime.dateTime.TimeOfDay;
									dt += timeSpan;

									BaseContext._Purposes.Add
										(
											new Purpose()
											{
												Name = ev.Name,
												Note = ev.Note,
												user = _user,
												Duration = ev.Duration,
												IDFrequency = ev.IDFrequency,
												Work = false
											}
										);
									BaseContext._EventTimes.Add
										(
											new EventTime()
											{
												purpose = ev,
												DoWork = _eventTime.DoWork,
												dateTime = dt,
												Work = _eventTime.Work,
											}
										);
								}
								else
								{ break; }
							}
						}
					}
				}
			}
			BaseContext.SaveChanges();
			System.Console.WriteLine("Поток StartThreadEachMouthPurpose завершен");
		}
		private void StartThreadEachYearPurpose()
		{
			foreach(Purpose ev in BaseContext._Purposes.Include("user"))
			{
				if(ev.user.ID == _user.ID)
				{
					_eventTime = BaseContext._EventTimes.Where(p => p.purpose == ev).FirstOrDefault(p => p.DoWork == 4);

					if(_eventTime != null)
					{
						if(_eventTime.dateTime.Day == _tempDate.date.Day && _eventTime.dateTime.Month == _tempDate.date.Month)
						{
							if(CheckDate(_eventTime))
							{
								System.DateTime dt = _tempDate.date;

								EventTime eventTime = BaseContext._EventTimes.Where(p => p.purpose == ev).FirstOrDefault(p => p.dateTime.DayOfYear == _tempDate.date.DayOfYear);
								if(eventTime == null)
								{
									System.TimeSpan timeSpan = _eventTime.dateTime.TimeOfDay;
									dt += timeSpan;

									BaseContext._Purposes.Add
										(
											new Purpose()
											{
												Name = ev.Name,
												Note = ev.Note,
												user = _user,
												Duration = ev.Duration,
												IDFrequency = ev.IDFrequency,
												Work = false
											}
										);
									BaseContext._EventTimes.Add
										(
											new EventTime()
											{
												purpose = ev,
												DoWork = _eventTime.DoWork,
												dateTime = dt,
												Work = _eventTime.Work,
											}
										);
								}
								else
								{ break; }
							}
						}
					}
				}
			}
			BaseContext.SaveChanges();
			System.Console.WriteLine("Поток StartThreadEachYearPurpose завершен");
		}

	}
}
