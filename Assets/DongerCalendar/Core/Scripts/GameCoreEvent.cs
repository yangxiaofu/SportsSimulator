using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Donger.Tools;

namespace Donger.BuckeyeEngine{
    public class GameCoreEvent : CoreEvent
    {
		public GameCoreEvent(string name, DateTime date){
			this.Name = name;
			this.Date = date;
			this.Day = date.Day;
			this.Month = date.Month;
			this.Year = date.Year;

			//Generates a unique ID for the class;
			_id = UniqueID.Generate();
		}

        public override void AddComponentTo(GameObject gameObjectTotAddTo)
        {
            _eventBehaviour = gameObjectTotAddTo.AddComponent<GameEventBehaviour>();
        }
    }

}
