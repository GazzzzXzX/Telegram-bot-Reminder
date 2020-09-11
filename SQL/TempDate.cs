using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Calendar.SQL
{
	class TempDate
	{
		[Key]
		public System.Int32 ID { get; set; }
		[Required]
		public User user { get; set; }

		public System.DateTime date { get; set; }
	}
}
