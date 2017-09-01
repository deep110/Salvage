using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : Singleton <PowerUpManager> {

	[System.Serializable]
	public class PowerUps {
		public GameObject shield;
		public GameObject verticalBeam;
		public GameObject horizontalBeam;
	}

	public PowerUps powerUps;

	private List<PowerUp> activePowerUps;
}
