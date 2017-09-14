using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton <UIManager> {

	public Transform scorePanel;
	public GameObject menuPanel;

	public GameObject hBeamButton;
	public GameObject vBeamButton;

	public bool HorizontalBeamButtonClicked {get; set;}
	public bool VerticalBeamButtonClicked {get;set;}

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

	public void OnHBeamButtonClicked() {
		HorizontalBeamButtonClicked = true;
		hBeamButton.GetComponent<Button>().interactable = false;
	}

	public void OnVBeamButtonClicked() {
		VerticalBeamButtonClicked = true;
		vBeamButton.GetComponent<Button>().interactable = false;
	}

	/// Also called when poweUp time is over
	public void HideBeamButton(GameObject beamButton) {
		// TODO: make it blink and disapper, or use any Particle effect
		beamButton.SetActive(false);
		beamButton.GetComponent<Button>().interactable = true;
	}
}
