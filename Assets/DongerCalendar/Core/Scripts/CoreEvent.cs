using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Donger.Tools;

namespace Donger.BuckeyeEngine
{
	public abstract class CoreEvent {
		protected string _id;
		public string ID{get{return _id;}}
		public DateTime	Date;
		public int Day;
		public int Month;
		public int Year;
		public string Name;
		protected EventBehaviour _eventBehaviour;
		public abstract void AddComponentTo(GameObject gameObjectToAddto);		
		public virtual void InitializeGameObject()
		{
			//Check if this has the right component on the gameobject.
			if (_eventBehaviour == null) Debug.LogError("The EventBehaviour is null.  You must use the AddComponentTo method first");	
		}
    }
}

