using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton <UIManager> {

	public Text scorePanel;
	public GameObject menuPanel;

	public GameObject hBeamButton;
	public GameObject vBeamButton;
	public GameObject platformClearPanel;

	public void OnPauseButtonClick() {
		Time.timeScale = 0;
		menuPanel.SetActive(true);
	}

	public void UpdateScoreText(int score) {
		scorePanel.text = score.ToString();
	}

	public void ShowPlatformClear() {
		platformClearPanel.SetActive (true);
		Invoke("HidePlatformClear", 2f);
	}

	private void HidePlatformClear() {
		platformClearPanel.SetActive (false);
	}
}
