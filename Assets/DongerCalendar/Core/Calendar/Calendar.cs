﻿using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Donger.BuckeyeEngine{
	//Track the current date in the game (done) 
	//Track all days in the calendar.  Needs to store events in a certain date. 
	//Move to a specific date. 
	[ExecuteInEditMode]
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

		public delegate void Updated(DateTime date);
		public event Updated OnUpdated;

		protected EventManager _eventManager;
		public EventManager EventManager{get{return _eventManager;}}


		protected virtual void OnEnable()
		{
			if (!Application.isPlaying)
			{
				_eventManager = GetComponent<EventManager>();
			}
		}

		protected virtual void Start()
		{
			if (Application.isPlaying)
			{
				_eventManager = GetComponent<EventManager>();
			}
		}

		public string HelpBox()
		{
			return "The Calendar is responsible for handling the dates in the simluation.";
		}
		
		///<summary>Updates the calendar values</summary>
		public virtual void UpdateCalendar(int year, int month, int day)
        {
            SelectedYear = year;
            SelectedMonth = month;
            SelectedDay = day;

            NotifyObservers(year, month, day);
        }

		///<summary>Resets the calendar to the default values</summary>
        public virtual void ResetToDefault()
        {
			UpdateCalendar(StartingYear, StartingMonth, StartingDay);
        }

		protected virtual void NotifyObservers(int year, int month, int day)
        {
            var date = new DateTime(year, month, day);
            if (OnUpdated != null) OnUpdated(date);
        }

    }
}

