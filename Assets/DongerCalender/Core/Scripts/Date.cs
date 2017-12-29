using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DongerCalendar.Core{
	[System.Serializable]
	public class Date{

		[Range(2000, 3000)]
		[SerializeField] int _year;
		public int Year{get{return _year;}}

		[Range(1, 12)]
		[SerializeField] int _month;
		public int Month {get{return _month;}}
		[Range(1, 30)]
		[SerializeField] int _day;
		public int Day{get {return _day;}}

		DateTime _date;
		public DateTime DateTime{
			get{return _date;}
			set{_date = value;}
		}
		
	#region Constructors
		public Date(int year, int month, int day)
		{
			_year = year;
			_month = month;
			_day = day;

			//This will throw an error if the date is invalid. 
			_date = new DateTime(year, month, day);
		}

		public Date(DateTime dateTime){
			_date = dateTime;
		}

	#endregion

	#region Public Methods

		///<summary>
		///Adds days to the current date that is set
		///</summary>
		public DateTime AddDays(int days)
		{
			return _date.AddDays(days);
		}

	#endregion
	}
}
