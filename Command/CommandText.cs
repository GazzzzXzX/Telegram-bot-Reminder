using System.Collections.Generic;


namespace Calendar
{
	class CommandText
	{
		public static System.String Start = "/start";

		public static System.String BackToStart = "BackToStart";

		#region Category
		public static System.String CategorySelection = "CategorySelection";
		public static System.String AddItemCategory = "AddItemCategory";
		public static System.String AddPicture = "AddPicture";
		public static System.String AddLink = "AddLink";
		public static System.String ShowItemCategory = "showItemCategory";
		public static System.String News = "News";
		public static System.String Sport = "Sport";
		public static System.String Notes = "Notes";
		public static System.String Cooking = "Cooking";
		public static System.String Documents = "Documents";
		public static System.String Video = "Video";
		public static System.String Entertainment = "Entertainment";
		public static System.String Finance = "Finance";
		public static System.String Travels = "Travels";
		public static System.String Cars = "Cars";
		public static System.String Buy = "Buy";
		public static System.String Anather = "Anather";
		public static System.String BackToCategory = "BackToCategory";

		public static System.String ChangeSecond = "CahangeSecond";
		public static System.String ChangeFirst = "CahngeFirst";
		public static System.String DeleteNotes = "DeleteNotes";
		public static System.String BackToNews = "BackToNews";

		public static Dictionary<System.Int32, System.String> keyValues = new Dictionary<System.Int32, System.String>();
		#endregion

		#region Calendar
		public static System.String Calendar = "Calendar";
		public static System.String Next = ">";
		public static System.String Back = "<";
		public static System.String ChouseData = "ChouseData";
		public static System.String ShowOneDayCalendar = "ShowOneDayCalendar";

		public static System.String BackToCalendar = "BackToCalendar";
		public static System.String BackToMenuReminder = "BackToMenuReminder";
		public static System.String AddTime = "AddTime";
		public static System.String AddReminderReiteration = "AddReminderReiteration"; // Повторение
		public static System.String AddPurooseReiteration = "AddPurooseReiteration"; // Повторение
		public static System.String AddEventReiteration = "AddEventReiteration"; // Повторение
		public static System.String Reiteration = "Reiteration";

		public static System.String EachDayReminder = "EachDayReminder";
		public static System.String EachWeakReminder = "EachWeakReminder";
		public static System.String EachMouthReminder = "EachMouthReminder";
		public static System.String EachYearReminder = "EachYearReminder";

		public static System.String BackToEvent = "BackToEvent";
		public static System.String BackToPurpose = "BackToPurpose";
		public static System.String BackToReminder = "BackToReminder";
		public static System.String BackToEventNotBusy = "BackToEventNotBusy";

		public static System.String EachDayPurpose = "EachDayPurpose";
		public static System.String EachWeakPurpose = "EachWeakPurpose";
		public static System.String EachMouthPurpose = "EachMouthPurpose";
		public static System.String EachYearPurpose = "EachYearPurpose";

		public static System.String EachDayEvent = "EachDayEvent";
		public static System.String EachWeakEvent = "EachWeakEvent";
		public static System.String EachMouthEvent = "EachMouthEvent";
		public static System.String EachYearEvent = "EachYearEvent";

		public static System.String AddReminder = "AddReminder";
		public static System.String AddPurpose = "AddPurpose";
		public static System.String AddEventBusy = "AddEventBusy";
		public static System.String AddEventNotBusy = "AddEventNotBusy";
		public static System.String AddEventNotBusy2 = "AddEventNotBusy2";


		public static System.String AddContextReminder = "AddContextReminder";
		public static System.String AddContextPurpose = "AddContextPurpose";
		public static System.String AddContextEvent = "AddContextEvent";

		public static System.String AddLocation = "AddLocation";

		public static System.String DurationPurpose = "DurationPurpose"; // Продолжительность
		public static System.String DurationPurposeTime15 = "DurationPurposeTime15"; // Продолжительность
		public static System.String DurationPurposeTime30 = "DurationPurposeTime30"; // Продолжительность
		public static System.String DurationPurposeTime1 = "DurationPurposeTime1"; // Продолжительность
		public static System.String DurationPurposeTime2 = "DurationPurposeTime2"; // Продолжительность

		public static System.String ShowEvent = "ShowEvent";
		public static System.String ShowPurpose = "ShowPurpose";
		public static System.String ShowReminder = "ShowReminder";

		
		public static System.String ChangeReminderName = "ChangeReminderName";
		public static System.String ChangeReminderNote = "ChangeReminderNote";
		public static System.String ChangeReminderReiteration = "ChangeReminderReiteration";
		public static System.String BackToChangeReminder = "BackToChangeReminder";

		public static System.String ChangePurposeName = "ChangePurposeName";
		public static System.String ChangePurposeNote = "ChangePurposeNote";
		public static System.String ChangePurposeReiteration = "ChangePurposeReiteration";
		public static System.String BackToChangePurpose = "BackToChangePurpose";

		public static System.String ChangeEventName = "ChangeEventName";
		public static System.String ChangeEventNote = "ChangeEventNote";
		public static System.String ChangeEventReiteration = "ChangeEventReiteration";
		public static System.String ChangeEventLocation = "ChangeEventLocation";
		public static System.String BackToChangeEvent = "BackToChangeEvent";


		public static System.String ChangeAllTime = "ChangeAllTime";
		public static System.String DeleteAllEvent = "DeleteAllEvent";

		public static System.String BackToChoseDate = "BackToChoseDate";

		public static System.String OrganizerSattisticDay = "OrganizerSattisticDay";

		public static Dictionary<System.Int32, System.String> ChouseAllEvent = new Dictionary<System.Int32, System.String>();

		public static Dictionary<System.Int32, System.String> MyData = new Dictionary<System.Int32, System.String>();
		public static Dictionary<System.Int32, System.String> ShowItemDayDayCalendar = new Dictionary<System.Int32, System.String>();
		#endregion

		#region Setting
		public static System.String StartSettingsOne = "StartSettingsOne";
		public static System.String StartSettingsTwo = "StartSettingsOne";
		public static System.String StartSettingsThree = "StartSettingsThree";
		public static System.String StartSettingsFour = "StartSettingsFour";

		public static System.String AddE_Mail = "AddE_Mail";
		public static System.String DeleteE_Mail = "DeleteE_Mail";
		public static System.String SubscribeToTheNewsletterE_Mail = "SubscribeToTheNewsletterE_Mail";
		public static System.String SubscribeToTheNewsletterTelegram = "SubscribeToTheNewsletterTelegram";
		public static System.String BackToSettingsOne = "BackToSettingsOne";
		public static System.String BackToSettingsTwo = "BackToSettingsTwo";
		public static System.String BackToSettingsThree = "BackToSettingsThree";
		public static System.String BackToSettingsFour = "BackToSettingsFour";
		public static System.String YesDeleteE_Mail = "YesDeleteE_Mail";
		public static System.String NoDeleteE_Mail = "NoDeleteE_Mail";
		#endregion

		#region Organizer
		public static System.String StartOrganizer = "StartOrganizer";
		public static System.String StartOrganizerTime = "StartOrganizerTime";
		public static System.String OrganizerTimeToWork = "OrganizerTimeToWork";
		public static System.String OrganizerTimeToRelaxation = "OrganizerTimeToRelaxation";
		public static System.String OrganizerSattisticMouth = "OrganizerSattisticMouth";
		public static System.String BackToOrganizer = "BackToOrganizer";
		public static System.String StopOrganizerTime = "StopOrganizerTime";
		public static System.String StartOrganizerTimeRelax = "StartOrganizerTimeRelax";
		#endregion

		//37 финансовая звитнисть пидприемства


	}
}