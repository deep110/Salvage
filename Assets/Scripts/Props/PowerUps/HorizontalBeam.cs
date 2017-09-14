using UnityEngine;

public class HorizontalBeam : PowerUp {

	private UIManager uiManager;
	private bool beamActive;

	protected override void Start() {
        base.Start();
        uiManager = UIManager.Instance;
    }

    public override void Collected() {
        base.Collected();

        uiManager.hBeamButton.SetActive(true);
    }

    public override void Tick() {
		if (uiManager.HorizontalBeamButtonClicked) {
			// beamActive = true;
			uiManager.HorizontalBeamButtonClicked = false;
			timeSinceStart = duration;
		}
		if (!beamActive) {
			base.Tick();
		}
    }

	public override void Ended() {
        uiManager.HideBeamButton(uiManager.hBeamButton);

        base.Ended();
    }
}
