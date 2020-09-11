using System.ComponentModel.DataAnnotations;

namespace Calendar.SQL
{
	class Event
	{
		[Key]
		public System.Int32 ID { get; set; }
		[Required]
		public User user { get; set; }

		public System.String Name { get; set; }

		public System.String Note { get; set; }

		public System.String Location { get; set; }

		public System.Int32 Count { get; set; }

		public System.Boolean Work { get; set; }

		public System.Boolean Busy { get; set; }
	}
}