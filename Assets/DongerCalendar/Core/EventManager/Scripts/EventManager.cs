using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;

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
            ClearEventTransform();

            //clear the events. 
            CoreEvents.Clear();

            //Get the events. 
            CoreEvents = GetEvents(date.DateTime);

            //Fill it back up with events if events exist.
            for (int i = 0; i < CoreEvents.Count; i++)
            {
                var eventObject = new GameObject(CoreEvents[i].Name);
                CoreEvents[i].AddComponentTo(eventObject);
                CoreEvents[i].InitializeGameObject();
                eventObject.transform.SetParent(_parentForEvents);
                eventObject.transform.localPosition = Vector3.zero;
            }
        }

		///<summary>Clears the event parent transform</summary>
        public void ClearEventTransform()
        {
			while(_parentForEvents.childCount != 0)
			{
             	DestroyImmediate(_parentForEvents.GetChild(0).gameObject);
         	}
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
					for (int i = 0; i < _numberOfEventsToGenerate; i++)
            		{
						var coreEvent = new GameCoreEvent("Game", beginDate.Date);
						Database.Add(coreEvent);
            		}
                    break;
                case EventType.Draft: 
					throw new NotImplementedException();
				case EventType.Practice:
					for (int i = 0; i < _numberOfEventsToGenerate; i++)
            		{
						var coreEvent = new PracticeCoreEvent("Practice", beginDate.Date);
						Database.Add(coreEvent);
            		}
					break;
				case EventType.FreeAgency:
					throw new NotImplementedException();					
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
