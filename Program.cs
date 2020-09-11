using System.IO;
using Calendar.Mail;
using Calendar.TelegramMessage;
using Calendar.Picture;

namespace Calendar
{
	class Program
	{
		static void Main(System.String[] args)
		{
			EmailScheduler.Start();
			TelegramMessageSheduler.Start();
			//779815529:AAEQVsHABl-V1rNhxX3vDnCo_r1xQjYczNU
			//781639864:AAE7Y4gc6ldOYlLGgfsHeaXU2V24QBMXyo0
			_ = new Bot("781639864:AAE7Y4gc6ldOYlLGgfsHeaXU2V24QBMXyo0");

			System.Console.ReadLine();	
		}
	}
}
