using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace DongerCalendar.Core{
	//Track the current date in the game (done) 
	//Track all days in the calendar.  Needs to store events in a certain date. 
	//Move to a specific date. 
	
	public class Calendar : DongerCalendarCoreBehaviour
	{
		//TODO: Testing.
		[Header("Current Date")]
		[Tooltip("Use this to set the current date in the game.")]
		[SerializeField] Date _date;

		///<summary> This is the current date</summary>
		public Date Date{get{return _date;}}
		
		void Start()
        {
            Initialization();
			AddDays(3);
        }

		public override string HelpBox()
		{
			return "The Calendar is responsible for handling the dates in the simluation.";
		}

		///<summary>
		///Handles the initialization
		///</summary>
        protected virtual void Initialization()
        {
			return;
        }

		///<summary> 
		/// Will Add Days to the current date.
		/// Returns DateTime
		///</summary>
		public DateTime AddDays(int days)
		{
			return _date.AddDays(days);
		}

		///<summary>
		/// Will set the current date to the preferred date.
		///</summary>
		public void SetDate(DateTime date)
		{
			_date.DateTime = date;
		}
    }
}

