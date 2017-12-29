using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DongerCalendar.Core{
	[CreateAssetMenu(menuName = "DongerCalendar/Create Event Database")]

	///<summary> 
	///This is the database in which all events will be stored.</summary>
	///</summary>

	public class EventsDatabase : ScriptableObject {
		[SerializeField] List<CoreEvents> _events = new List<CoreEvents>();
		
		///<summary>
		///This will find all of the events on a particular Date
		///</summary>
		public List<CoreEvents> GetEventsOn(Date date)
		{
			List<CoreEvents> _myEvents = new List<CoreEvents>();
			for(int i = 0; i < _events.Count; i++)
			{
				if (_events[i].Date.Year == date.Year && _events[i].Date.Month == date.Month && _events[i].Date.Day == date.Day){
					_myEvents.Add(_events[i]);
				}
			}
			return _myEvents;
		}
	}
}


