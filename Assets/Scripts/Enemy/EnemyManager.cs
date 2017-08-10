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

	void OnEnable() {
		EventManager.GameOverEvent += gameOver;
	}

	void Start () {
		playerOneController = PlayerManager.Instance.playerOneController;
		StartCoroutine(ManageBall());
	}

	void OnDisable() {
		EventManager.GameOverEvent -= gameOver;
	}

	private void gameOver() {
		isGameOver = true;
	}
	

	private IEnumerator ManageBall() {
		// intialize necessary variables
		var ballPooler = new ObjectPooler(enemies.ball, 4);

		// wait for some time to spawn enemies
        yield return new WaitForSeconds(5f);

        // spawn enemies till game is not over
        while (!isGameOver) {
			print(playerOneController.GetLastStablePosition());
			yield return new WaitForSeconds(2f);
        }
		
	}
}
