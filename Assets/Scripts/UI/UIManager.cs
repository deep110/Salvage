using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton <UIManager> {

	public Transform scorePanel;
	public GameObject menuPanel;

	private Text scoreText;
	private Text platformText;

	protected override void Awake() {
		base.Awake();

		scoreText = scorePanel.GetChild(0).GetComponent<Text>();
		platformText = scorePanel.GetChild(1).GetComponent<Text>();
	}

	public void OnPauseButtonClick() {
		Time.timeScale = 0;
		menuPanel.SetActive(true);
	}

	public void UpdateScoreText(int score) {
		scoreText.text = score.ToString();
	}

	public void UpdatePlatformsClimbed(int platformsClimbed) {
		platformText.text = platformsClimbed.ToString();
	}
}
