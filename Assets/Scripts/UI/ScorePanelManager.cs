using UnityEngine;
using UnityEngine.UI;

public class ScorePanelManager : MonoBehaviour {

	private Text scoreText;
	private Text platformText;

	private int score;

	void OnEnable() {
		EventManager.CoinCollectEvent += coinCollected;
		EventManager.PlatformClimbEvent += platformClimbed;

		scoreText = transform.GetChild(0).GetComponent<Text>();
		platformText = transform.GetChild(1).GetComponent<Text>();
	}

	private void coinCollected() {
		score++;
		scoreText.text = score.ToString();
	}

	private void platformClimbed(int platformNo) {
		platformText.text = platformNo.ToString();
	}

	void OnDisable() {
		EventManager.CoinCollectEvent -= coinCollected;
		EventManager.PlatformClimbEvent -= platformClimbed;
	}
}
