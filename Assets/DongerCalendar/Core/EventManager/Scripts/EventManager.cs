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
		public EventsDatabase Database;

		[Tooltip("This is the transform that the events will be parented to.  If this is null, then it'll automatically create an Events parent.")]
		[SerializeField] protected Transform _parentForEvents;

		[Space]
		[Header("Event Generator")]
		[SerializeField] protected EventType _eventType;

        [Tooltip("This will generate a number of events within the dates below.")]
		[SerializeField] int _numberOfEventsToGenerate = 1;

		public string EventDate;
		public DateTime selectedDate;
		protected Calendar _calendar;
		protected const string EVENTS = "Events";
		public List<CoreEvent> CoreEvents = new List<CoreEvent>();

		public string HelpBox()
		{
			return "The Event Manager is responsible for handling the events in the calendar.  The Calendar component is required.";
		}

		protected virtual void OnEnable()
		{
			//@EditorMode
			if (!Application.isPlaying)
			{
				//Find the calendar, and register to it's notificalendar
				_calendar = GetComponent<Calendar>();
				_calendar.OnUpdated += OnCalendarUpdated;
			}
		}
	
		protected virtual void Start()
		{
			//@Runtime
			if (Application.isPlaying)
			{
				//If parent for events is null, then create it.
				if (!_parentForEvents) _parentForEvents = new GameObject(EVENTS).transform;
			}
		}

		///<summary>Will refresh the core events for the particular day.</summary>
		public virtual void RefreshCoreEvents(Date date)
		{
			CoreEvents.Clear();
			CoreEvents = GetEvents(date.DateTime);
		}
		
		///<summary>Callback from the Calendar.cs</summary>
        protected virtual void OnCalendarUpdated(DateTime date)
        {
			//Update the variables. 
            selectedDate = date;
			var dateString = date.Month + "/" + date.Day + "/" + date.Year;
			EventDate = dateString;

			//Will update the display.	
			RefreshCoreEvents(new Date(date.Year, date.Month, date.Day));
        }

		///<summary>Auto Generate Events in the Event Manager executed primary in the EventManagerEditor</summary>
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

        protected virtual void GenerateEvents(string eventName, DateParser beginDate)
        {
            for (int i = 0; i < _numberOfEventsToGenerate; i++)
            {
                var coreEvent = new CoreEvent(eventName, beginDate.Date);
                Database.Add(coreEvent);
            }
        }

		///<summary>Get number of events from this date.</summary>
		public virtual List<CoreEvent> GetEvents(DateTime date)
		{
			return Database.Find(date);
		}

        public virtual void RemoveEvent(string coreEventID)
        {
            Database.Remove(coreEventID);
        }
    }
}
