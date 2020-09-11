using System.ComponentModel.DataAnnotations;

namespace Calendar.SQL
{
	class Purpose //--- Цель
	{
		[Key]
		public System.Int32 ID { get; set; }
		[Required]
		public User user { get; set; }

		public System.Int32 IDFrequency { get; set; }

		public System.String Name { get; set; }

		public System.String Note { get; set; }

		public System.Boolean Work { get; set; }

		public System.TimeSpan Duration { get; set; }

	}
}