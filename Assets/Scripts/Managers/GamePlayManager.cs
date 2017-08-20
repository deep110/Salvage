using UnityEngine;

/**
* Act as top level manager for the game
* handles the game between start and till
* game is over.
*/
public class GamePlayManager : Singleton <GamePlayManager> {

	public int score;
	public GameObject gameOverDialog;

	void Start() {
		Time.timeScale = 1;

		EventManager.CoinCollectEvent += onCoinCollected;
		EventManager.GameOverEvent += onGameOver;
	}

	void OnDisable() {
		EventManager.CoinCollectEvent -= onCoinCollected;
		EventManager.GameOverEvent -= onGameOver;
	}

	private void onCoinCollected() {
		score++;
	}

	private void onGameOver() {
		// pause the game
		Time.timeScale = 0;

		// show the gameOver dialog
		gameOverDialog.SetActive(true);
	}
}
