using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Calendar.SQL;
using Microsoft.EntityFrameworkCore;
using Quartz;


namespace Calendar.Mail
{
	class E_MailPurpose : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			DataBaseContext BaseContext = new DataBaseContext();
			foreach(Purpose reminder in BaseContext._Purposes.Include("user"))
			{
				EventTime eventTime = BaseContext._EventTimes.Where(p => p.purpose == reminder).FirstOrDefault();
				if(eventTime != null)
				{
					if(eventTime.dateTime != null)
					{
						if(eventTime.dateTime.Date == System.DateTime.Today
								&& eventTime.dateTime.Hour == System.DateTime.Now.Hour
								&& eventTime.dateTime.Minute == System.DateTime.Now.Minute)
						{
							if(reminder.user.SendMessageE_Mail == true && reminder.user.EmailAdress != null)
							{
								// отправитель - устанавливаем адрес и отображаемое в письме имя
								MailAddress from = new MailAddress("telegrambotgaz@gmail.com", "CalendarBot");
								// кому отправляем
								MailAddress to = new MailAddress(reminder.user.EmailAdress);
								// создаем объект сообщения
								MailMessage m = new MailMessage(from, to);
								// тема письма
								m.Subject = "Цель";
								// текст письма
								
								m.Body = "<h2>" + "Название: " + reminder.Name + "<br>";
								m.Body += "Описание: " + reminder.Note + "<br>";
								m.Body += "Продолжительность: " + reminder.Duration + "<br>";
								m.Body += "</h2>";
								
								// письмо представляет код html
								m.IsBodyHtml = true;

								//m.Attachments.Add(@"Путь к файлу!");

								// адрес smtp-сервера и порт, с которого будем отправлять письмо
								SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
								// логин и пароль
								smtp.Credentials = new NetworkCredential("telegrambotgaz@gmail.com", "123456qweASD123456");
								smtp.EnableSsl = true;
								await smtp.SendMailAsync(m);
								System.Console.WriteLine("Сообщение отправлено!");
							}
						}
					}
				}
			}
			BaseContext.Dispose();
		}
	}
}
