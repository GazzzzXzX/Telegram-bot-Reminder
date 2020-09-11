using System.ComponentModel.DataAnnotations;

namespace Calendar.SQL
{
	class Statistics
	{
		[Key]
		public System.Int32 ID { get; set; }
		[Required]
		public User user { get; set; }

		public System.DateTime Day { get; set; }

		public System.TimeSpan TimeInDayToWork { get; set; }

		public System.TimeSpan TimeInDayToRelax { get; set; }

		public System.Byte[] image { get; set; }
	}
}
