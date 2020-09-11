﻿using System.ComponentModel.DataAnnotations;

namespace Calendar
{
	class BDFinance
	{
		[Key]
		public System.Int32 ID { get; set; }
		[Required]
		public User user { get; set; }

		public System.String Name { get; set; }

		public System.String link { get; set; }

		public System.Boolean Work { get; set; }

		//public ICollection<User> _User { get; set; }

		//public BDFinance()
		//{
		//	_User = new List<User>();
		//}
	}
}