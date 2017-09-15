using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PowerUpManager : Singleton <PowerUpManager> {

	[System.Serializable]
	public class PowerUps {
		public GameObject shield;
		public GameObject verticalBeam;
		public GameObject horizontalBeam;
	}

	public PowerUps powerUps;

	private Transform _camera;

	private List<PowerUp> activePowerUps;
	private Dictionary<GameObject, int> powerUpWeights;
	private bool isGameOver;


	protected override void Awake() {
        base.Awake();

        _camera = GameObject.FindWithTag("MainCamera").transform;
		powerUpWeights = new Dictionary<GameObject, int> {
            { powerUps.shield, 2 },
            { powerUps.verticalBeam, 10 },
            { powerUps.horizontalBeam, 10 }
		};

		activePowerUps = new List<PowerUp>();
    }

	void Start() {
		EventManager.GameOverEvent += gameOver;

		StartCoroutine(GeneratePowerUps());
	}

	void Update() {
		if (!isGameOver) {
			for (int i = 0; i < activePowerUps.Count; i++) {
				PowerUp item = activePowerUps[i];
			    if (item.IsActive) {
					item.Tick();
				} else {
					activePowerUps.Remove(item);
					item.Ended();
				}
			}
		}
	}

	public void AddActivePowerUp(PowerUp powerUp) {
		if (!powerUp.IsActive || !powerUp.IsFakeActive) {
			for (int i = 0; i < activePowerUps.Count; i++) {
				if (activePowerUps[i].powerUpName == powerUp.powerUpName) {
					activePowerUps[i].ResetTime();

					// make this powerup fake active
					powerUp.MakeFakeActive();
					return;
				}
			}
			powerUp.Collected();
			activePowerUps.Add(powerUp);
		}
	}

	void onDisable() {
		EventManager.GameOverEvent -= gameOver;
		StopAllCoroutines();
	}

	private IEnumerator GeneratePowerUps() {
		while(!isGameOver) {
			yield return new WaitForSeconds(10f);

			GameObject selected = WeightedRandomizer<GameObject>.From(powerUpWeights).TakeOne();
			selected.transform.localPosition = new Vector3(
				Random.Range(-2.5f, 2.5f),
				_camera.position.y + 6f,
				0);
			Instantiate(selected);
		}
	}

	private void gameOver() {
		isGameOver = true;
	}
}
