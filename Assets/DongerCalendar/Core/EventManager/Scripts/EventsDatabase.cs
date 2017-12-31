using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Donger.BuckeyeEngine{
	[CreateAssetMenu(menuName = "DongerCalendar/Create Event Database")]

	///<summary> 
	///This is the database in which all events will be stored.</summary>
	///</summary>
	public class EventsDatabase : ScriptableObject {

		///<summary>Returns a helpbox string description of this class.</summary>
		public string HelpBox()
		{
			return "This is meant to display the events on the selected day.  You can find the events for the day after selecting a day.";
		}
		[SerializeField] List<CoreEvent> _events = new List<CoreEvent>();
		
		///<summary>This list of Core Events</summary>
		public List<CoreEvent> Events{get{return _events;}}

		public void Add(CoreEvent coreEvent)
		{
			_events.Add(coreEvent);
		}

		
		public void Remove(string id)
		{
			_events.RemoveAll(x => x.ID == id);
		}

		public List<CoreEvent> Find(DateTime date)
		{
			List<CoreEvent> events = new List<CoreEvent>();

			for(int i = 0; i < _events.Count; i++)
			{
				if (_events[i].Year == date.Year && _events[i].Month == date.Month && _events[i].Day == date.Day){
					events.Add(_events[i]);
				}
			}

			return events;
		}

		public void Clear()
		{
			_events.Clear();
		}
	}
}


