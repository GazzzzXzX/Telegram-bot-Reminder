using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calendar.SQL
{
	class EventTime // Многие к одному.
	{
		[Key]
		public System.Int32 ID { get; set; }


		public Event _event { get; set; }

		public Reminder reminder { get; set; }

		public Purpose purpose { get; set; }

		public System.DateTime dateTime { get; set; } // Один

		public System.Boolean Work { get; set; } // Дефолтное значение

		public System.Int32 DoWork { get; set; } // Режим повторения 
	}
}