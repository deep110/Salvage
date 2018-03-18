using UnityEngine;

public class VerticalBeam : PowerUp {

    protected const int K_layerMask = 1 << 0;

    private GameObject beam;
    private float midPosition;

    public override void Collected() {
        base.Collected();

        // activate the beam at collected position
        beam = transform.GetChild(0).gameObject;

        midPosition = Camera.main.transform.position.y;
        beam.transform.position = new Vector3(transform.position.x, midPosition, 0);     
        beam.SetActive(true);
    }

    public override void Ended() {
        // pick up all crystals in beam range
        Collider2D[] colliders = Physics2D.OverlapBoxAll(
            new Vector2(transform.position.x, midPosition),
            new Vector2(0.63f, 9f),
            0, K_layerMask
        );

        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].CompareTag("Crystal")) {
                colliders[i].GetComponent<Crystal>().Collect();
            }
        }

        beam.SetActive(false);
        base.Ended();
    }
}