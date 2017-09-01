using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : Singleton <PowerUpManager> {

	[System.Serializable]
	public class PowerUps {
		public GameObject shield;
		public GameObject verticalRailgun;
		public GameObject horizontalRailgun;
	}

	public PowerUps powerUps;
}
