using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Donger.Tools;

namespace Donger.BuckeyeEngine{
	[Serializable]
	public class CoreEvent{
		private string _id;
		public string ID{get{return _id;}}
		public DateTime	Date;
		public int Day;
		public int Month;
		public int Year;
		public string Name;
		public CoreEvent(string name, DateTime date)
		{
			this.Name = name;
			this.Date = date;
			this.Day = date.Day;
			this.Month = date.Month;
			this.Year = date.Year;

			//Generates a unique ID for the class;
			_id = UniqueID.Generate();
		}
	}
}

