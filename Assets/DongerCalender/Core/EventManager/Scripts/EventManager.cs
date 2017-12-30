using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO: Should find a way to generate events in the game. 
//TODO: Find a way to store the events as a List
//TODO: Create a class that has dates and what teh event type of.


namespace Donger.BuckeyeEngine{
	[RequireComponent(typeof(Calendar))]
	[ExecuteInEditMode]
	public class EventManager : MonoBehaviour
	{
		[Tooltip("The database that stores all of the events in the game.")]
		[SerializeField] protected EventsDatabase _database;

		[Tooltip("This is the transform that the events will be parented to.  If this is null, then it'll automatically create an Events parent.")]
		[SerializeField] protected Transform _parentForEvents;

		[Space]
		[Header("Event Generator")]
		[SerializeField] protected EventType _eventType;

		[Tooltip("This will generate a number of events within the dates below.")]
		[SerializeField] int _numberOfEventsToGenerate = 1;

		[Tooltip("Format the Date by MM-DD-YYYY")]
		public string EventDate;
		
		public DateTime selectedDate;
		protected Calendar _calendar;
		protected const string EVENTS = "Events";
		public string HelpBox()
		{
			return "The Event Manager is responsible for handling the events in the calendar.  The Calendar component is required.";
		}
	
		void Start()
		{
			//If parent for events is null, then create it.
			if (!_parentForEvents) _parentForEvents = new GameObject(EVENTS).transform;

			//Find the calendar
			_calendar = GetComponent<Calendar>();
			_calendar.OnUpdated += OnCalendarUpdated;
		}
		
		///<summary>Callback from the Calendar.cs</summary>
        void OnCalendarUpdated(DateTime date)
        {
            selectedDate = date;
        }

        public void SimulateEventsForCurrentDay()
		{
			throw new NotImplementedException();
		}

		///<summary>Auto Generate Events in the Event Manager</summary>
		public virtual void GenerateEvents()
		{
			//Parse the dates into a readable format
			var beginDate = new DateParser(EventDate);
			

			//Do specific type of event depending on the event type.
			switch(_eventType)
			{
                case EventType.Game:
                    GenerateEvents("Game", beginDate);
                    break;
                case EventType.Draft: 
					GenerateEvents("Draft", beginDate);
					break;
				case EventType.Practice:
					GenerateEvents("Practice", beginDate);
					break;
				case EventType.FreeAgency:
					GenerateEvents("Free Agency", beginDate);
					break;
			}
		}

        private void GenerateEvents(string eventName, DateParser beginDate)
        {
            for (int i = 0; i < _numberOfEventsToGenerate; i++)
            {
                var coreEvent = new CoreEvent(eventName, beginDate.Date);
                _database.Add(coreEvent);
            }
        }
    }
}
