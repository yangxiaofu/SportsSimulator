using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Donger.BuckeyeEngine;

namespace Game.Core{
	public class GameEventBehaviour : EventBehaviour{
        public override void Simulate()
        {
            print("Simulate the event");
        }

      
	}
}

