using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calendar
{
	class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public System.Int32 ID { get; set; }

		public System.String EmailAdress { get; set; }

		public System.Int32 Work { get; set; }

		public System.Boolean SendMessageE_Mail { get; set; }

		public System.Boolean SendMessageTelegram { get; set; }

		public System.TimeSpan TimeToWork { get; set; }

		public System.TimeSpan TimeToRelaxation { get; set; }

		public System.Boolean TimeWork { get; set; }
	}
}
