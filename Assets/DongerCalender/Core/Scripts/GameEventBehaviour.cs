using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DongerCalendar.Core{
	///<summary>
	///This behaviour is responsible for simulating each game in the game scene.
	///</summary>
	public class GameEventBehaviour : EventBehaviour{

		public override void Initialize(Date date)
		{
			base.Initialize(date);
		}

        public override void Simulate()
        {
            //Game Event Simulation will occurr here.
			print("Simulates the game " + this.gameObject.name);
        }
    }

}
