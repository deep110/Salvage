using UnityEngine;

/**
* Act as top level manager for the game
* handles the game between start and till
* game is over.
*/
public class GamePlayManager : Singleton <GamePlayManager> {

	public int score;
	public int platformsClimbed;

	public GameObject gameOverDialog;

	private UIManager uiManager;

	void Start() {
		Time.timeScale = 1;
		uiManager = UIManager.Instance;

		EventManager.CoinCollectEvent += onCoinCollected;
		EventManager.PlatformClimbEvent += onPlatformClimbed;
		EventManager.GameOverEvent += onGameOver;
		EventManager.PlatformClearEvent += onPlatformClear;
	}

	void OnDisable() {
		EventManager.CoinCollectEvent -= onCoinCollected;
		EventManager.PlatformClimbEvent -= onPlatformClimbed;
		EventManager.GameOverEvent -= onGameOver;
		EventManager.PlatformClearEvent -= onPlatformClear;
	}

	private void onCoinCollected() {
		score++;
		uiManager.UpdateScoreText(score);
	}

	private void onPlatformClimbed(int platforms) {
		platformsClimbed = platforms;
		uiManager.UpdatePlatformsClimbed(platforms);
	}

	private void onGameOver() {
		// pause the game
		Time.timeScale = 0;

		// show the gameOver dialog
		gameOverDialog.SetActive(true);
	}

	private void onPlatformClear() {
		//increase the score by 5
		score += 5;
		//display the platform clear panel
		uiManager.ShowPlatformClear();
		//update the ui score
		uiManager.UpdateScoreText(score);
	}
}
