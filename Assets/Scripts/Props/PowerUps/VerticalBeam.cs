using UnityEngine;

public class VerticalBeam : PowerUp {

    private UIManager uiManager;
    private bool beamActive;

    protected override void Start() {
        base.Start();
        uiManager = UIManager.Instance;
    }

    public override void Collected() {
        base.Collected();

        uiManager.vBeamButton.SetActive(true);
    }

    public override void Tick() {
        if (uiManager.VerticalBeamButtonClicked) {
            // beamActive = true;
            uiManager.VerticalBeamButtonClicked = false;
            timeSinceStart = duration;
        }
        if (!beamActive) {
            base.Tick();
        }
    }

    public override void Ended() {
        uiManager.HideBeamButton(uiManager.vBeamButton);

        base.Ended();
    }
}