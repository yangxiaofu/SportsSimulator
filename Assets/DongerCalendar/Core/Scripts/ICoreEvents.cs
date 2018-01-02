using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Donger.BuckeyeEngine{
	public interface ICoreEvent {
		void AddComponentTo(GameObject gameObjectTotAddTo);
		string ID{get;}
		DateTime Date{get;set;}
		int Day{get;set;}
		int Month{get;set;}
		int Year{get;set;}
		string Name{get;set;}

	}
}

