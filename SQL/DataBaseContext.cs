using System;
using System.Collections.Generic;
using Calendar.SQL;
using Microsoft.EntityFrameworkCore;

namespace Calendar
{
	class DataBaseContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=CalendarBot; Trusted_Connection=True");
		}

		public DbSet<User> _User { get; set; }
		public DbSet<BDNews> _News { get; set; }
		public DbSet<BDSport> _Sport { get; set; }
		public DbSet<BDNotes> _Notes { get; set; }
		public DbSet<BDCooking> _Cooking { get; set; }
		public DbSet<BDDocuments> _Documets { get; set; }
		public DbSet<BDVideo> _Video { get; set; }
		public DbSet<BDEntertainment> _Entertainment { get; set; }
		public DbSet<BDFinance> _Finance { get; set; }
		public DbSet<BDTravels> _Travels { get; set; }
		public DbSet<BDCars> _Cars { get; set; }
		public DbSet<BDBuy> _Buy { get; set; }
		public DbSet<BDAnather> _Anather { get; set; }
		public DbSet<Event> _Events { get; set; }
		public DbSet<EventTime> _EventTimes { get; set; }
		public DbSet<Purpose> _Purposes { get; set; }
		public DbSet<Statistics> _Statistics { get; set; }
		public DbSet<Reminder> _Reminders { get; set; }
		public DbSet<TempDate> _TempDate { get; set; }

	}
}
