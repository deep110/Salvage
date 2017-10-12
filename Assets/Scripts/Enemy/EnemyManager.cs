using System.Collections;
using UnityEngine;

public class EnemyManager : Singleton <EnemyManager> {

	[System.Serializable]
	public class Enemies {
		public GameObject ball;
		public GameObject book;
		public GameObject bee;
	}

	public Enemies enemies;

	private Character playerOneController;
	private bool isGameOver;
	private int platformNumber;

	void OnEnable() {
		EventManager.GameOverEvent += gameOver;
		EventManager.PlatformClimbEvent += platformClimbed;

		isGameOver = false;
		playerOneController = PlayerManager.Instance.playerOneController;

		// start the coroutines
		StartCoroutine(ManageBallAndBee());
		StartCoroutine(ManageBook());
		StartCoroutine(ManageBee());
	}

	void OnDisable() {
		EventManager.GameOverEvent -= gameOver;
		EventManager.PlatformClimbEvent -= platformClimbed;

		// stop coroutines
		StopAllCoroutines();
	}

	private IEnumerator ManageBallAndBee() {
		var ballPooler = new ObjectPooler(enemies.ball, 3);
		// var beePooler = new ObjectPooler(enemies.bee, 2);

		// wait for some time to spawn enemies
        yield return new WaitForSeconds(10f);

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

			yield return new WaitForSeconds(Random.Range(10f, 20f));
        }
	}

	private IEnumerator ManageBook() {
		var bookPooler = new ObjectPooler(enemies.book, 2);

		yield return new WaitWhile(() => platformNumber <= 5);

		while (!isGameOver) {
			Vector2 lastStablePos = playerOneController.GetLastStablePosition();

			bookPooler.Spawn(new Vector3(lastStablePos.x, lastStablePos.y + 8f, 0));
			yield return new WaitForSeconds(Random.Range(4.5f, 7f));
        }
	}

	private IEnumerator ManageBee() {
		var beePooler = new ObjectPooler(enemies.bee, 2);

        yield return new WaitWhile(() => platformNumber <= 8);

        // spawn enemies till game is not over
        while (!isGameOver) {
			Vector2 lastStablePos = playerOneController.GetLastStablePosition();

			GameObject bee = beePooler.SpawnInActive(new Vector3(0, lastStablePos.y, 0));
			if (lastStablePos.x > 0) {
				bee.GetComponent<Bee>().Fly(true);
			} else {
				bee.GetComponent<Bee>().Fly(false);
			}
			bee.SetActive(true);

			yield return new WaitForSeconds(Random.Range(8f, 15f));
        }
	}

	private void platformClimbed(int platformNo) {
		platformNumber = platformNo;
	}

	private void gameOver() {
		isGameOver = true;
	}
}
