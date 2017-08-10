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

	// corutines
	private IEnumerator ballCoroutine;

	void OnEnable() {
		EventManager.GameOverEvent += gameOver;
	}

	void Start () {
		isGameOver = false;
		playerOneController = PlayerManager.Instance.playerOneController;
		ballCoroutine = ManageBall();

		// start the coroutines
		StartCoroutine(ballCoroutine);
	}

	void OnDisable() {
		EventManager.GameOverEvent -= gameOver;

		// stop coroutines
		StopCoroutine(ballCoroutine);
	}

	private IEnumerator ManageBall() {
		// intialize necessary variables
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

	private void gameOver() {
		isGameOver = true;
	}
}
