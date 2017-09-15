using UnityEngine;

public class VerticalBeam : PowerUp {

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
        beamButton = UIManager.Instance.vBeamButton;
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
                Destroy(dragableSprite);
                beamState = BeamState.NONE;

                fireBeam();
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
        Debug.Log("Vertical Beam Fired");
    }

    private Vector3 getTouchPosition() {
        Vector3 position = (Application.isMobilePlatform)
                ? Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)
                : Camera.main.ScreenToWorldPoint(Input.mousePosition);

        position.z = 0;
        return position;
    }
}