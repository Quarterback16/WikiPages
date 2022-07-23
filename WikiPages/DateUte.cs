using System;

namespace WikiPages
{
    public class DateUte
	{
		public static string StdDate(DateTime theDate)
		{
			var formattedDate = theDate.ToString("yyyy-MM-dd");
			return formattedDate;
		}
	}
}
