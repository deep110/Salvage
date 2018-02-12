using UnityEngine;

public class SpikeTriggerController : MonoBehaviour {

    public float speed = 1f;

    private Vector2 position;

    void Start() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        position = transform.position;
    }

    void Update() {
        if (speed > 0 && transform.position.x < -4.4f) {
            position.x = 4.3f;
            transform.position = position;
        } else if (speed < 0 && transform.position.x > 4.3f) {
            position.x = -4.4f;
            transform.position = position;
        }
    }

    public void SetSpeed(float speed) {
        this.speed = speed;
    }
}
