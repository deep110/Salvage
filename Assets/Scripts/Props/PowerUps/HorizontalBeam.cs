using UnityEngine;

public class HorizontalBeam : PowerUp {

	public GameObject dragableSprite;

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
                    dragableSprite = Instantiate(dragableSprite, getTouchPosition(), Quaternion.identity);
                }
                break;

            case BeamState.ONGOING:
                dragableSprite.transform.position = getTouchPosition();
                if (!beamButton.GetComponent<PointerListener>().Pressed) {
                    beamState = BeamState.FIRE;
                }
                break;

            case BeamState.FIRE:
                PlayerManager.Instance.isBeamPowerUpActive = false;
                Vector3 finalPosition = dragableSprite.transform.position;
                Destroy(dragableSprite);

                fireBeam(finalPosition);
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

    private void fireBeam(Vector3 beamPosition) {
		beamState = BeamState.NONE;
    }

    private Vector3 getTouchPosition() {
    	Vector3 position = (Application.isMobilePlatform)
    			? Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)
    			: Camera.main.ScreenToWorldPoint(Input.mousePosition);

     	position.z = 0;
     	return position;
    }
}
