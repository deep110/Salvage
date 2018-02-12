using System.Collections;
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
    private GameObject beam;
    private BeamState beamState;

    protected const int K_layerMask = 1 << 0;


    protected override void Start() {
        base.Start();
        beam = transform.GetChild(0).gameObject;
        beamButton = UIManager.Instance.vBeamButton;
    }

    public override void Collected() {
        base.Collected();

        beamButton.SetActive(true);
        beamState = BeamState.START;
    }

    public override void Tick() {
        switch (beamState) {

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
                    PlayerManager.Instance.isBeamPowerUpActive = false;

                    StartCoroutine(fireBeam(dragableSprite.transform.position.x));
                    Destroy(dragableSprite);
                }
                break;

            case BeamState.NONE:
                timeSinceStart = duration;
                base.Tick();
                break;
        }
    }

    public override void Ended() {
        beamButton.SetActive(false);
        base.Ended();
    }

    private Vector3 getTouchPosition() {
        Vector3 position = (Application.isMobilePlatform)
                ? Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)
                : Camera.main.ScreenToWorldPoint(Input.mousePosition);

        position.z = 0;
        return position;
    }

    private IEnumerator fireBeam(float beamPositionX) {
        float midPosition = Camera.main.transform.position.y;
        beam.transform.position = new Vector3(beamPositionX, midPosition, 0);
        beam.SetActive(true);

        yield return new WaitForSeconds(0.6f);

        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(beamPositionX, midPosition),
                                    new Vector2(0.63f, 9f), 0, K_layerMask);

        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].CompareTag("Crystal")) {
                colliders[i].GetComponent<Crystal>().Collect();
            }
        }

        beam.SetActive(false);
        beamState = BeamState.NONE;
    }
}