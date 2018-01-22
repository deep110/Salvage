using System.Collections;
using UnityEngine;

public class EnemyManager : Singleton <EnemyManager> {

	[System.Serializable]
	public class Enemies {
		public GameObject ball;
		public GameObject copter;
		public GameObject tank;
		public GameObject laserGrid;
		public GameObject spikes;
	}

	public Enemies enemies;

	private Character playerOneController;
	private bool isGameOver;
	private int platformNumber;

	//for if laser is on, no enemy should spawn
	private bool isLaserOn;

	void OnEnable() {
		EventManager.GameOverEvent += gameOver;
		EventManager.PlatformClimbEvent += platformClimbed;

		isGameOver = false;
		playerOneController = PlayerManager.Instance.playerOneController;

		// start the coroutines
		StartCoroutine(ManageSpikes());
		StartCoroutine(ManageBall());
		StartCoroutine(ManageCopter());
		StartCoroutine(ManageTank());
		StartCoroutine(ManageLaser());
	}

	void OnDisable() {
		EventManager.GameOverEvent -= gameOver;
		EventManager.PlatformClimbEvent -= platformClimbed;

		// stop coroutines
		StopAllCoroutines();
	}

	private IEnumerator ManageSpikes() {
		var spikesPooler = new ObjectPooler (enemies.spikes, 3);
		yield return new WaitWhile(() => platformNumber <= 5);

		while (!isGameOver) {
			Vector2 lastStablePos = playerOneController.GetLastStablePosition();

			if (!isLaserOn) {
				if (platformNumber >= 30) {
					GameObject spikes = spikesPooler.SpawnInActive (new Vector3 (0, lastStablePos.y + 1.65f * 4, 0));
					spikes.SetActive (true);
				}

				GameObject spikes2 = spikesPooler.SpawnInActive (new Vector3 (0, lastStablePos.y + 1.65f * 3, 0));
				spikes2.transform.localScale = (2 * Random.Range(0, 1) - 1) * spikes2.transform.localScale;
				spikes2.GetComponentInChildren<SpikeTriggerController> ().SetSpeed ((2 * Random.Range(0, 1) - 1) * 1);
				spikes2.SetActive (true);
			}

			yield return new WaitForSeconds(Random.Range(10f, 20f));
		}
	}

	private IEnumerator ManageBall() {
		var ballPooler = new ObjectPooler(enemies.ball, 2);

		// wait for some time to spawn enemies
        yield return new WaitForSeconds(5f);

        // spawn enemies till game is not over
        while (!isGameOver) {
			Vector2 lastStablePos = playerOneController.GetLastStablePosition();

			if (!isLaserOn) {
				GameObject ball = ballPooler.SpawnInActive(new Vector3 (0, lastStablePos.y, 0));
				ball.GetComponent<Ball>().Roll((lastStablePos.x > 0));
				ball.SetActive (true);
			}

			yield return new WaitForSeconds(Random.Range(10f, 20f));
        }
	}

	private IEnumerator ManageCopter() {
		var copterPooler = new ObjectPooler(enemies.copter, 2);

		yield return new WaitWhile(() => platformNumber <= 5);

		while (!isGameOver) {
			Vector2 lastStablePos = playerOneController.GetLastStablePosition();

			if (!isLaserOn) {
				copterPooler.Spawn(new Vector3 (lastStablePos.x, lastStablePos.y + 8f, 0));
			}
			yield return new WaitForSeconds(Random.Range (4.5f, 7f));
        }
	}

	private IEnumerator ManageTank() {
		var tankPooler = new ObjectPooler(enemies.tank, 1);

        yield return new WaitWhile(() => platformNumber <= 8);

        // spawn enemies till game is not over
        while (!isGameOver) {
			Vector2 lastStablePos = playerOneController.GetLastStablePosition();

			if (!isLaserOn) {
				GameObject tank = tankPooler.SpawnInActive (new Vector3 (0, lastStablePos.y, 0));
				tank.GetComponent<Tank>().Move((lastStablePos.x > 0));
				tank.SetActive (true);
			}
			yield return new WaitForSeconds(Random.Range(10f, 16f));
        }
	}

	private IEnumerator ManageLaser() {
		yield return new WaitWhile(() => platformNumber <= 15);
		while (!isGameOver) {
			isLaserOn = true;
			float timeForLaser = enemies.laserGrid.GetComponent<LaserManager>().Activate();
			yield return new WaitForSeconds(timeForLaser);

			isLaserOn = false;
			yield return new WaitForSeconds(Random.Range (30f, 40f));
		}
	}

	private void platformClimbed(int platformNo) {
		platformNumber = platformNo;
	}

	private void gameOver() {
		isGameOver = true;
	}

}
