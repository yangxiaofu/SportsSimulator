using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Donger.BuckeyeEngine{
	public class DateParser {

		///<summary>The delimiters separating the dayes</summary>
		private char[] delimiterChars = {'-', '/'};

		///<summary>The date created</summary>
		public DateTime Date;

		///<summary>The Day</summary>/
		public int Day;

		///<summary>The Month</summary>
		public int Month;

		///<summary>The Year</summary>
		public int Year;

		///<summary>Format should be MM-DD-YYYY or MM/DD/YYYY</summary>
		public DateParser (string date)
		{
			string[] dates = date.Split(delimiterChars);
			if (dates.Length > 3) Debug.LogError("You do not have the proper format.  It should be MM-DD-YYYY or MM/DD-YYYY");

			Day = Convert.ToInt16(dates[1]);
			Month = Convert.ToInt16(dates[0]);
			Year = Convert.ToInt32(dates[2]);

			Date = new DateTime(Year, Month, Day);
		}
	}
}

