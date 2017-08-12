using UnityEngine;
using System.Collections;

public class EnemyManager : Singleton <EnemyManager> {

	[System.Serializable]
	public class Enemies {
		public GameObject ball;
	}

	public Enemies enemies;

	private PlayerController playerOneController;
	private bool isGameOver;
	private int platformNumber;

	void OnEnable() {
		EventManager.GameOverEvent += gameOver;
		EventManager.PlatformClimbEvent += platformClimbed;
	}

	void Start () {
		isGameOver = false;
		playerOneController = PlayerManager.Instance.playerOneController;

		// start the coroutines
		StartCoroutine(ManageBall());
		StartCoroutine(ManageBoulder());
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
        yield return new WaitForSeconds(5f);

        // spawn enemies till game is not over
        while (!isGameOver) {
			Vector2 lastStablePos = playerOneController.GetLastStablePosition();

			GameObject ball = ballPooler.SpawnInActive(new Vector3(0, lastStablePos.y, 0));
			if (lastStablePos.x > 0) {
				ball.GetComponent<Ball>().Roll(true);
			} else {
				ball.GetComponent<Ball>().Roll(false);
			}
			ball.SetActive(true);

			yield return new WaitForSeconds(3.5f);
        }
	}

	private IEnumerator ManageBoulder() {
		yield return new WaitWhile(() => platformNumber <= 12);

		Debug.Log("start bouldering");
	}

	private void platformClimbed(int platformNo) {
		platformNumber = platformNo;
	}

	private void gameOver() {
		isGameOver = true;
	}
}
