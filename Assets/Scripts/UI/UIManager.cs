using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject gameOverDialog;

	private Text scoreText;
	private Text platformText;

	private int score;

	void OnEnable() {
		EventManager.GameOverEvent += gameOver;
		EventManager.CoinCollectEvent += coinCollected;
		EventManager.PlatformClimbEvent += platformClimbed;

		scoreText = transform.GetChild(0).GetComponent<Text>();
		platformText = transform.GetChild(1).GetComponent<Text>();
	}

	void OnDisable() {
		EventManager.GameOverEvent -= gameOver;
		EventManager.CoinCollectEvent -= coinCollected;
		EventManager.PlatformClimbEvent -= platformClimbed;
	}

	private void coinCollected() {
		score++;
		scoreText.text = score.ToString();
	}

	private void platformClimbed(int platformNo) {
		platformText.text = platformNo.ToString();
	}

	private void gameOver() {
		// show the gameOver dialog
		gameOverDialog.SetActive(true);
	}
}
