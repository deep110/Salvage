using System.Collections;
using UnityEngine;

public class EnemyManager : Singleton <EnemyManager> {

	[System.Serializable]
	public class Enemies {
		public GameObject ball;
		public GameObject book;
		public GameObject tank;
	}

	public GameObject laserSet;

	public Enemies enemies;

	private Character playerOneController;
	private bool isGameOver;
	private int platformNumber;
	//for if laser is on, no enemy should spawn
	private bool isLaserOn = false;

	void OnEnable() {
		EventManager.GameOverEvent += gameOver;
		EventManager.PlatformClimbEvent += platformClimbed;

		isGameOver = false;
		playerOneController = PlayerManager.Instance.playerOneController;

		// start the coroutines
		StartCoroutine (ManageBall ());
		StartCoroutine (ManageBook ());
		StartCoroutine (ManageTank ());
		StartCoroutine (ManageLaser ());
	}

	void OnDisable() {
		EventManager.GameOverEvent -= gameOver;
		EventManager.PlatformClimbEvent -= platformClimbed;

		// stop coroutines
		StopAllCoroutines();
	}

	private IEnumerator ManageBall() {
		var ballPooler = new ObjectPooler(enemies.ball, 3);

		// wait for some time to spawn enemies
        yield return new WaitForSeconds(10f);

        // spawn enemies till game is not over
        while (!isGameOver) {
			Vector2 lastStablePos = playerOneController.GetLastStablePosition();

			if (!isLaserOn) {
				GameObject ball = ballPooler.SpawnInActive (new Vector3 (0, lastStablePos.y, 0));
				if (lastStablePos.x > 0) {
					ball.GetComponent<Ball> ().Roll (true);
				} else {
					ball.GetComponent<Ball> ().Roll (false);
				}
				ball.SetActive (true);
			}

			yield return new WaitForSeconds(Random.Range(10f, 20f));
        }
	}

	private IEnumerator ManageBook() {
		var bookPooler = new ObjectPooler(enemies.book, 2);

		yield return new WaitWhile(() => platformNumber <= 5);

		while (!isGameOver) {
			Vector2 lastStablePos = playerOneController.GetLastStablePosition();

			if (!isLaserOn) {
				bookPooler.Spawn (new Vector3 (lastStablePos.x, lastStablePos.y + 8f, 0));
			}
			yield return new WaitForSeconds (Random.Range (4.5f, 7f));
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
				if (lastStablePos.x > 0) {
					tank.GetComponent<Tank> ().Move (true);
				} else {
					tank.GetComponent<Tank> ().Move (false);
				}
				tank.SetActive (true);
			}
			yield return new WaitForSeconds(Random.Range(10f, 16f));
        }
	}

	private IEnumerator ManageLaser() {
		yield return new WaitForSeconds (1f);
		while (!isGameOver) {
			isLaserOn = true;
			float timeForLaser = laserSet.GetComponent<LaserManager> ().Activate ();
			Invoke ("LaserIsOff", timeForLaser);
			yield return new WaitForSeconds (Random.Range (30f, 40f));
		}
	}

	private void platformClimbed(int platformNo) {
		platformNumber = platformNo;
	}

	private void gameOver() {
		isGameOver = true;
	}

	private void LaserIsOff() {
		isLaserOn = false;
	}

}
