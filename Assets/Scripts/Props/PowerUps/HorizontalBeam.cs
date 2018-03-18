using UnityEngine;

public class HorizontalBeam : PowerUp {

    protected const int K_layerMask = 1 << 0;

    private GameObject beam;

    public override void Collected() {
        base.Collected();

        // activate the beam at collected position
        beam = transform.GetChild(0).gameObject;
        beam.transform.position = new Vector3(0, transform.position.y - 0.5f, 0);
        beam.SetActive(true);
    }

    public override void Ended() {
        // pick up all crystals in beam range
        Collider2D[] colliders = Physics2D.OverlapBoxAll(
            new Vector2(0, transform.position.y - 0.5f),
            new Vector2(4f, 0.63f),
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
