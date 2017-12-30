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
		[SerializeField] List<CoreEvent> _events = new List<CoreEvent>();
		
		///<summary>This list of Core Events</summary>
		public List<CoreEvent> Events{get{return _events;}}
		
		///<summary>
		///This will find all of the events on a particular Date
		///</summary>
		public List<CoreEvent> GetEventsOn(Date date)
		{
			List<CoreEvent> _myEvents = new List<CoreEvent>();
			for(int i = 0; i < _events.Count; i++)
			{
				if (_events[i].Date.Year == date.Year && _events[i].Date.Month == date.Month && _events[i].Date.Day == date.Day){
					_myEvents.Add(_events[i]);
				}
			}
			return _myEvents;
		}

		public void Add(CoreEvent coreEvent){
			_events.Add(coreEvent);
		}

		
		public void Remove(string id)
		{
			Debug.Log("Removing " + id);
		}

		public void Clear()
		{
			_events.Clear();
		}
	}
}


