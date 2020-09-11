using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Calendar.SQL;
using Telegram.Bot.Types;

namespace Calendar.Picture
{
	class PictureB
	{
		public System.Boolean SetPicture(User users, System.Object message, Statistics statistics, TempDate date, DataBaseContext baseContext)
		{
			CallbackQuery _message = message as CallbackQuery;
			if(date.date != null)
			{
				if(statistics.Day == date.date)
				{
					System.TimeSpan timeSpan = statistics.TimeInDayToWork;
					System.TimeSpan timeSpan2 = statistics.TimeInDayToRelax;

					System.Int32 one = ((timeSpan.Hours * 3600) + (timeSpan.Minutes * 60) + timeSpan.Seconds);
					System.Int32 two = ((timeSpan2.Hours * 3600) + (timeSpan2.Minutes * 60) + timeSpan2.Seconds);

					System.Int32 x = (one * 360) / 86400;
					System.Int32 y = (two * 360) / 86400;

					System.String inaction = "Бездействие: " + (new System.TimeSpan(24, 0, 0) - (timeSpan + timeSpan2));
					System.String work = "Работа: " + timeSpan;
					System.String relax = "Отдых: " + timeSpan2;
					System.String name = ":Продуктивность дня";

					Bitmap myBitmap = new Bitmap(800, 800);//Создаем картинку
					Graphics g = Graphics.FromImage(myBitmap);

					DrawEllipse(g, x, y);
					DrawString(g, inaction, work, relax, name);

					System.String str = @"E:\Repo C#\Calendar\Calendar\" + _message.From.Id + ".jpeg";

					Save(str, myBitmap, statistics, baseContext);
					Delete(str);
					return true;
				}
			}
			return false;
		}

		private void DrawEllipse(Graphics g, System.Int32 x, System.Int32 y)
		{
			Rectangle myRectangle = new Rectangle(50, 50, 650, 650);

			g.FillEllipse(new SolidBrush(Color.Red), myRectangle);
			g.FillPie(new SolidBrush(Color.Green), myRectangle, 0, x);
			g.FillPie(new SolidBrush(Color.Yellow), myRectangle, x, y);
			g.DrawEllipse(new Pen(Color.White), myRectangle);
			g.DrawPie(new Pen(Color.White), myRectangle, 0, x);
			g.DrawPie(new Pen(Color.White), myRectangle, x, y);

		}
		private void DrawString(Graphics g, System.String inaction, System.String work, System.String relax, System.String name)
		{
			Font drawFont = new Font("Arial", 16);
			SolidBrush drawBrush = new SolidBrush(Color.White);
			System.Single i = 700.0F;
			System.Single j = 700.0F;

			// Set format of string.
			StringFormat drawFormat = new StringFormat();
			drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

			// Draw string to screen.
			g.DrawString(inaction, drawFont, drawBrush, i, j, drawFormat);
			g.DrawString(work, drawFont, drawBrush, i, j + 30, drawFormat);
			g.DrawString(relax, drawFont, drawBrush, i, j + 60, drawFormat);
			g.DrawString(name, drawFont, drawBrush, 250, 10, drawFormat);
		}

		private void Save(System.String str, Bitmap myBitmap, Statistics statistics, DataBaseContext baseContext)
		{
			myBitmap.Save(str, System.Drawing.Imaging.ImageFormat.Jpeg);

			MemoryStream ms = new MemoryStream();
			myBitmap.Save(ms, ImageFormat.Jpeg);
			System.Byte[] bmpBytes = ms.ToArray();

			statistics.image = bmpBytes;
			baseContext.SaveChanges();
		}
		private void Delete(System.String str)
		{
			if(System.IO.File.Exists(str))
			{
				// If file found, delete it    
				System.IO.File.Delete(str);
			}
		}
	}
}
