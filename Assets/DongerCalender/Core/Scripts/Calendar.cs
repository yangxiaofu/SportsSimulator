using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Donger.BuckeyeEngine{
	//Track the current date in the game (done) 
	//Track all days in the calendar.  Needs to store events in a certain date. 
	//Move to a specific date. 
	
	public class Calendar : MonoBehaviour
	{
		//TODO: Testing.
		[Header("Current Date")]
		[Tooltip("Use this to set the current date in the game.")]

		public int StartingYear = 2018;
		public int SelectedYear;

		public int StartingMonth = 1;
		public int SelectedMonth; 

		public int StartingDay = 1;
		public int SelectedDay;

		public string HelpBox()
		{
			return "The Calendar is responsible for handling the dates in the simluation.";
		}
    }
}

