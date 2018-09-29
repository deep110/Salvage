using UnityEngine;

public class Crystal : MonoBehaviour {

    public Vector2 fallVelocity = new Vector2(0, -3);
    public GameObject crystalTeleport;

    private bool isFalling;
    private float timePassed;
    private Rigidbody2D _rigidbody;

    void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable() {
        isFalling = false;
    }

    public void Fall() {
        if (!isFalling) {
            isFalling = true;
            _rigidbody.velocity = fallVelocity;
            timePassed = Time.timeSinceLevelLoad;

            // play sound
            AudioManager.Instance.PlaySound("crystal_strike");
        } else if (Time.timeSinceLevelLoad - timePassed > 0.05f) {
            Collect();
        }
    }

    public void Collect() {
        // play sound
        AudioManager.Instance.PlaySound("crystal_teleport");

        Platform platform = GetComponent<Transform>().GetComponentInParent<Platform>();
        if (platform)
            platform.SetCrystalFall();
        
        EventManager.CrystalCollected();

        // instantiate the teleport particle system
        Instantiate(crystalTeleport, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);

        isFalling = false;

        // disable the crystal
        gameObject.SetActive(false);
    }

}
