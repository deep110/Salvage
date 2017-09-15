using UnityEngine;

public class HorizontalBeam : PowerUp {

	private enum BeamState {
		START,
		ONGOING,
		FIRE,
		NONE
	}

	private GameObject beamButton;
	private BeamState beamState;


	protected override void Start() {
        base.Start();
        beamButton = UIManager.Instance.hBeamButton;
    }

    public override void Collected() {
        base.Collected();

        beamButton.SetActive(true);
        beamState = BeamState.START;
    }

    public override void Tick() {
		switch(beamState) {

            case BeamState.START:
                base.Tick();
                if (beamButton.GetComponent<PointerListener>().Pressed) {
                    beamState = BeamState.ONGOING;
                    PlayerManager.Instance.isBeamPowerUpActive = true;
                }
                break;

            case BeamState.ONGOING:
                handleButtonDrag();
                if (!beamButton.GetComponent<PointerListener>().Pressed) {
                    beamState = BeamState.FIRE;
                }
                break;

            case BeamState.FIRE:
                PlayerManager.Instance.isBeamPowerUpActive = false;
                fireBeam();
                beamState = BeamState.NONE;
                break;

            case BeamState.NONE:
            	timeSinceStart = duration;
                base.Tick();
                break;
        }
    }

	public override void Ended() {
        UIManager.HideBeamButton(beamButton);
        base.Ended();
    }

    private void fireBeam() {
		Debug.Log("Horizontal Beam Fired");
    }

    private void handleButtonDrag() {
  //   	Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// pointerPos.x = mousePos.x;
		// pointerPos.y = mousePos.y;
    }
}
