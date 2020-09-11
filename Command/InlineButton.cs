using System;
using System.Collections.Generic;
using Calendar.Command;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Calendar
{
	class InlineButton
	{
		public InlineKeyboardMarkup Start = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Выбрать категорию", CallbackData = CommandText.CategorySelection },
				new InlineKeyboardButton{ Text = "Календарь", CallbackData = CommandText.Calendar }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Органайзер", CallbackData = CommandText.StartOrganizer},
				new InlineKeyboardButton{ Text = "Настройки", CallbackData = CommandText.StartSettingsOne}
			}

		};

		#region Category
		public InlineKeyboardMarkup Category = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Новости", CallbackData = CommandText.News },
				new InlineKeyboardButton{ Text = "Спорт", CallbackData = CommandText.Sport }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Записки", CallbackData = CommandText.Notes },
				new InlineKeyboardButton{ Text = "Кулинария", CallbackData = CommandText.Cooking }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Документы", CallbackData = CommandText.Documents },
				new InlineKeyboardButton{ Text = "Видео", CallbackData = CommandText.Video },
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Развлечения", CallbackData = CommandText.Entertainment },
				new InlineKeyboardButton{ Text = "Финансы", CallbackData = CommandText.Finance }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Путешествия", CallbackData = CommandText.Travels },
				new InlineKeyboardButton{ Text = "Автомобили", CallbackData = CommandText.Cars }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Покупки", CallbackData = CommandText.Buy },
				new InlineKeyboardButton{ Text = "Другое", CallbackData = CommandText.Anather }
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToStart }
			}
		};

		public InlineKeyboardMarkup AddMenu = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить картинку", CallbackData = CommandText.AddPicture},
				new InlineKeyboardButton { Text = "Добавить содержание", CallbackData = CommandText.AddLink}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToCategory }
			}
		};

		public InlineKeyboardMarkup News = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить запись", CallbackData = CommandText.AddItemCategory},
				new InlineKeyboardButton { Text = "Просмотреть записи", CallbackData = CommandText.ShowItemCategory}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToCategory }
			}
		};

		public InlineKeyboardMarkup Sport = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить запись", CallbackData = CommandText.AddItemCategory},
				new InlineKeyboardButton { Text = "Просмотреть записи", CallbackData = CommandText.ShowItemCategory}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToCategory }
			}
		};		

		public InlineKeyboardMarkup Notes = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить запись", CallbackData = CommandText.AddItemCategory},
				new InlineKeyboardButton { Text = "Просмотреть записи", CallbackData = CommandText.ShowItemCategory}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToCategory }
			}
		};
		

		public InlineKeyboardMarkup Cooking = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить запись", CallbackData = CommandText.AddItemCategory},
				new InlineKeyboardButton { Text = "Просмотреть записи", CallbackData = CommandText.ShowItemCategory}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToCategory }
			}
		};

		public InlineKeyboardMarkup Documents = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить запись", CallbackData = CommandText.AddItemCategory},
				new InlineKeyboardButton { Text = "Просмотреть записи", CallbackData = CommandText.ShowItemCategory}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToCategory }
			}
		};

		public InlineKeyboardMarkup Video = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить запись", CallbackData = CommandText.AddItemCategory},
				new InlineKeyboardButton { Text = "Просмотреть записи", CallbackData = CommandText.ShowItemCategory}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToCategory }
			}
		};

		public InlineKeyboardMarkup Entertainment = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить запись", CallbackData = CommandText.AddItemCategory},
				new InlineKeyboardButton { Text = "Просмотреть записи", CallbackData = CommandText.ShowItemCategory}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToCategory }
			}
		};

		public InlineKeyboardMarkup Finance = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить запись", CallbackData = CommandText.AddItemCategory},
				new InlineKeyboardButton { Text = "Просмотреть записи", CallbackData = CommandText.ShowItemCategory}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToCategory }
			}
		};

		public InlineKeyboardMarkup Travels = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить запись", CallbackData = CommandText.AddItemCategory},
				new InlineKeyboardButton { Text = "Просмотреть записи", CallbackData = CommandText.ShowItemCategory}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToCategory }
			}
		};

		public InlineKeyboardMarkup Cars = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить запись", CallbackData = CommandText.AddItemCategory},
				new InlineKeyboardButton { Text = "Просмотреть записи", CallbackData = CommandText.ShowItemCategory}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToCategory }
			}
		};

		public InlineKeyboardMarkup Buy = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить запись", CallbackData = CommandText.AddItemCategory},
				new InlineKeyboardButton { Text = "Просмотреть записи", CallbackData = CommandText.ShowItemCategory}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToCategory }
			}
		};

		public InlineKeyboardMarkup Anather = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton { Text = "Добавить запись", CallbackData = CommandText.AddItemCategory},
				new InlineKeyboardButton { Text = "Просмотреть записи", CallbackData = CommandText.ShowItemCategory}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToCategory }
			}
		};

		public InlineKeyboardMarkup ShowCategory = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToCategory }
			}
		};

		public InlineKeyboardMarkup BackToCategory = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToCategory }
			}
		};

		public InlineKeyboardMarkup ControlItem = new InlineKeyboardButton[][]
		{
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Изменить содержание", CallbackData = CommandText.ChangeSecond},
				new InlineKeyboardButton{ Text = "Изменить заголовок", CallbackData = CommandText.ChangeFirst}
			},
			new InlineKeyboardButton[]
			{
				new InlineKeyboardButton{ Text = "Удалить заметку", CallbackData = CommandText.DeleteNotes},
				new InlineKeyboardButton{ Text = "Назад", CallbackData = CommandText.BackToNews }
			}
		};
		#endregion

		#region ShowCategory
		public InlineKeyboardMarkup NewsShow(System.Object message, DataBaseContext baseContext) //выделение динамических кнопок
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;
			System.Int32 temp = 0;
			System.Boolean temp2 = true;

			CommandText.keyValues.Clear();

			foreach(BDNews news in baseContext._News.Include("user"))
			{
				if(news.user.ID == mes.From.Id)
				{
					temp++;
					if(temp < 2)
					{
						if(temp2 == true)
						{
							list.Add(new List<InlineKeyboardButton>());
							temp2 = false;
						}
					}
					else
					{
						temp = 0;
						temp2 = true;
					}
					CommandText.keyValues.Add(news.ID, news.Name.ToString());
					list[list.Count - 1].Add(new InlineKeyboardButton()
					{
						Text = news.Name.ToString(),
						CallbackData = news.ID + " " + CommandText.keyValues[news.ID] });
				}
			}

			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Назад",
				CallbackData = CommandText.BackToCategory
			});

			return new InlineKeyboardMarkup(list);
		}

		public InlineKeyboardMarkup SportShow(System.Object message, DataBaseContext baseContext) //выделение динамических кнопок
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;
			System.Int32 temp = 0;
			System.Boolean temp2 = true;


			CommandText.keyValues.Clear();

			foreach(BDSport sport in baseContext._Sport.Include("user"))
			{
				if(sport.user.ID == mes.From.Id)
				{
					temp++;
					if(temp < 2)
					{
						if(temp2 == true)
						{
							list.Add(new List<InlineKeyboardButton>());
							temp2 = false;
						}
					}
					else
					{
						temp = 0;
						temp2 = true;
					}
					CommandText.keyValues.Add(sport.ID, sport.Name.ToString());
					list[list.Count - 1].Add(new InlineKeyboardButton() { Text = sport.Name.ToString(), CallbackData = sport.ID + " " + CommandText.keyValues[sport.ID] });
				}
			}



			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Назад",
				CallbackData = CommandText.BackToCategory
			});

			return new InlineKeyboardMarkup(list);
		}

		public InlineKeyboardMarkup NotesShow(System.Object message, DataBaseContext baseContext) //выделение динамических кнопок
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;
			System.Int32 temp = 0;
			System.Boolean temp2 = true;


			CommandText.keyValues.Clear();

			foreach(BDNotes notes in baseContext._Notes.Include("user"))
			{
				if(notes.user.ID == mes.From.Id)
				{
					temp++;
					if(temp < 2)
					{
						if(temp2 == true)
						{
							list.Add(new List<InlineKeyboardButton>());
							temp2 = false;
						}
					}
					else
					{
						temp = 0;
						temp2 = true;
					}
					CommandText.keyValues.Add(notes.ID, notes.Name.ToString());
					list[list.Count - 1].Add(new InlineKeyboardButton() { Text = notes.Name.ToString(), CallbackData = notes.ID + " " + CommandText.keyValues[notes.ID] });
				}
			}



			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Назад",
				CallbackData = CommandText.BackToCategory
			});

			return new InlineKeyboardMarkup(list);
		}

		public InlineKeyboardMarkup CookingShow(System.Object message, DataBaseContext baseContext) //выделение динамических кнопок
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;
			System.Int32 temp = 0;
			System.Boolean temp2 = true;


			CommandText.keyValues.Clear();

			foreach(BDCooking cooking in baseContext._Cooking.Include("user"))
			{
				if(cooking.user.ID == mes.From.Id)
				{
					temp++;
					if(temp < 2)
					{
						if(temp2 == true)
						{
							list.Add(new List<InlineKeyboardButton>());
							temp2 = false;
						}
					}
					else
					{
						temp = 0;
						temp2 = true;
					}
					CommandText.keyValues.Add(cooking.ID, cooking.Name.ToString());
					list[list.Count - 1].Add(new InlineKeyboardButton() { Text = cooking.Name.ToString(), CallbackData = cooking.ID + " " + CommandText.keyValues[cooking.ID] });
				}
			}



			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Назад",
				CallbackData = CommandText.BackToCategory
			});

			return new InlineKeyboardMarkup(list);
		}

		public InlineKeyboardMarkup DocumentsShow(System.Object message, DataBaseContext baseContext) //выделение динамических кнопок
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;
			System.Int32 temp = 0;
			System.Boolean temp2 = true;


			CommandText.keyValues.Clear();

			foreach(BDDocuments documents in baseContext._Documets.Include("user"))
			{
				if(documents.user.ID == mes.From.Id)
				{
					temp++;
					if(temp < 2)
					{
						if(temp2 == true)
						{
							list.Add(new List<InlineKeyboardButton>());
							temp2 = false;
						}
					}
					else
					{
						temp = 0;
						temp2 = true;
					}
					CommandText.keyValues.Add(documents.ID, documents.Name.ToString());
					list[list.Count - 1].Add(new InlineKeyboardButton() { Text = documents.Name.ToString(), CallbackData = documents.ID + " " + CommandText.keyValues[documents.ID] });
				}
			}



			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Назад",
				CallbackData = CommandText.BackToCategory
			});

			return new InlineKeyboardMarkup(list);
		}

		public InlineKeyboardMarkup VideoShow(System.Object message, DataBaseContext baseContext) //выделение динамических кнопок
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;
			System.Int32 temp = 0;
			System.Boolean temp2 = true;


			CommandText.keyValues.Clear();

			foreach(BDVideo video in baseContext._Video.Include("user"))
			{
				if(video.user.ID == mes.From.Id)
				{
					temp++;
					if(temp < 2)
					{
						if(temp2 == true)
						{
							list.Add(new List<InlineKeyboardButton>());
							temp2 = false;
						}
					}
					else
					{
						temp = 0;
						temp2 = true;
					}
					CommandText.keyValues.Add(video.ID, video.Name.ToString());
					list[list.Count - 1].Add(new InlineKeyboardButton() { Text = video.Name.ToString(), CallbackData = video.ID + " " + CommandText.keyValues[video.ID] });
				}
			}



			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Назад",
				CallbackData = CommandText.BackToCategory
			});

			return new InlineKeyboardMarkup(list);
		}

		public InlineKeyboardMarkup EntertainmentShow(System.Object message, DataBaseContext baseContext) //выделение динамических кнопок
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;
			System.Int32 temp = 0;
			System.Boolean temp2 = true;


			CommandText.keyValues.Clear();

			foreach(BDEntertainment entertainment in baseContext._Entertainment.Include("user"))
			{
				if(entertainment.user.ID == mes.From.Id)
				{
					temp++;
					if(temp < 2)
					{
						if(temp2 == true)
						{
							list.Add(new List<InlineKeyboardButton>());
							temp2 = false;
						}
					}
					else
					{
						temp = 0;
						temp2 = true;
					}
					CommandText.keyValues.Add(entertainment.ID, entertainment.Name.ToString());
					list[list.Count - 1].Add(new InlineKeyboardButton() { Text = entertainment.Name.ToString(), CallbackData = entertainment.ID + " " + CommandText.keyValues[entertainment.ID] });
				}
			}



			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Назад",
				CallbackData = CommandText.BackToCategory
			});

			return new InlineKeyboardMarkup(list);
		}

		public InlineKeyboardMarkup FinanceShow(System.Object message, DataBaseContext baseContext) //выделение динамических кнопок
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;
			System.Int32 temp = 0;
			System.Boolean temp2 = true;


			CommandText.keyValues.Clear();

			foreach(BDFinance finance in baseContext._Finance.Include("user"))
			{
				if(finance.user.ID == mes.From.Id)
				{
					temp++;
					if(temp < 2)
					{
						if(temp2 == true)
						{
							list.Add(new List<InlineKeyboardButton>());
							temp2 = false;
						}
					}
					else
					{
						temp = 0;
						temp2 = true;
					}
					CommandText.keyValues.Add(finance.ID, finance.Name.ToString());
					list[list.Count - 1].Add(new InlineKeyboardButton() { Text = finance.Name.ToString(), CallbackData = finance.ID + " " + CommandText.keyValues[finance.ID] });
				}
			}



			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Назад",
				CallbackData = CommandText.BackToCategory
			});

			return new InlineKeyboardMarkup(list);
		}

		public InlineKeyboardMarkup TravelsShow(System.Object message, DataBaseContext baseContext) //выделение динамических кнопок
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;
			System.Int32 temp = 0;
			System.Boolean temp2 = true;


			CommandText.keyValues.Clear();

			foreach(BDTravels travels in baseContext._Travels.Include("user"))
			{
				if(travels.user.ID == mes.From.Id)
				{
					temp++;
					if(temp < 2)
					{
						if(temp2 == true)
						{
							list.Add(new List<InlineKeyboardButton>());
							temp2 = false;
						}
					}
					else
					{
						temp = 0;
						temp2 = true;
					}
					CommandText.keyValues.Add(travels.ID, travels.Name.ToString());
					list[list.Count - 1].Add(new InlineKeyboardButton() { Text = travels.Name.ToString(), CallbackData = travels.ID + " " + CommandText.keyValues[travels.ID] });
				}
			}



			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Назад",
				CallbackData = CommandText.BackToCategory
			});

			return new InlineKeyboardMarkup(list);
		}

		public InlineKeyboardMarkup CarsShow(System.Object message, DataBaseContext baseContext) //выделение динамических кнопок
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;
			System.Int32 temp = 0;
			System.Boolean temp2 = true;


			CommandText.keyValues.Clear();

			foreach(BDCars cars in baseContext._Cars.Include("user"))
			{
				if(cars.user.ID == mes.From.Id)
				{
					temp++;
					if(temp < 2)
					{
						if(temp2 == true)
						{
							list.Add(new List<InlineKeyboardButton>());
							temp2 = false;
						}
					}
					else
					{
						temp = 0;
						temp2 = true;
					}
					CommandText.keyValues.Add(cars.ID, cars.Name.ToString());
					list[list.Count - 1].Add(new InlineKeyboardButton() { Text = cars.Name.ToString(), CallbackData = cars.ID + " " + CommandText.keyValues[cars.ID] });
				}
			}



			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Назад",
				CallbackData = CommandText.BackToCategory
			});

			return new InlineKeyboardMarkup(list);
		}

		public InlineKeyboardMarkup BuyShow(System.Object message, DataBaseContext baseContext) //выделение динамических кнопок
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;
			System.Int32 temp = 0;
			System.Boolean temp2 = true;


			CommandText.keyValues.Clear();

			foreach(BDBuy buy in baseContext._Buy.Include("user"))
			{
				if(buy.user.ID == mes.From.Id)
				{
					temp++;
					if(temp < 2)
					{
						if(temp2 == true)
						{
							list.Add(new List<InlineKeyboardButton>());
							temp2 = false;
						}
					}
					else
					{
						temp = 0;
						temp2 = true;
					}
					CommandText.keyValues.Add(buy.ID, buy.Name.ToString());
					list[list.Count - 1].Add(new InlineKeyboardButton() { Text = buy.Name.ToString(), CallbackData = buy.ID + " " + CommandText.keyValues[buy.ID] });
				}
			}



			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Назад",
				CallbackData = CommandText.BackToCategory
			});

			return new InlineKeyboardMarkup(list);
		}

		public InlineKeyboardMarkup AnatherShow(System.Object message, DataBaseContext baseContext) //выделение динамических кнопок
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();
			CallbackQuery mes = message as CallbackQuery;
			System.Int32 temp = 0;
			System.Boolean temp2 = true;


			CommandText.keyValues.Clear();

			foreach(BDAnather anather in baseContext._Anather.Include("user"))
			{
				if(anather.user.ID == mes.From.Id)
				{
					temp++;
					if(temp < 2)
					{
						if(temp2 == true)
						{
							list.Add(new List<InlineKeyboardButton>());
							temp2 = false;
						}
					}
					else
					{
						temp = 0;
						temp2 = true;
					}
					CommandText.keyValues.Add(anather.ID, anather.Name.ToString());
					list[list.Count - 1].Add(new InlineKeyboardButton()
					{ Text = anather.Name.ToString(),
						CallbackData = anather.ID + " " + CommandText.keyValues[anather.ID]
					});
				}
			}



			list.Add(new List<InlineKeyboardButton>());
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Назад",
				CallbackData = CommandText.BackToCategory
			});

			return new InlineKeyboardMarkup(list);
		}

		#endregion
	}
}
