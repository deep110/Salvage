using UnityEngine;

public class PlatformSpike : Enemy {

    public Sprite [] spikes;

    public float height = 0.3f;
    public float speed = 0.7f;

    private enum State {
        CLOSE, CLOSE_UP,
        OPEN, OPEN_UP
    };

    private State currentState;
    private Vector2 position = Vector2.zero;
    private float timeElasped;

    private const string SPIKE_TAG = "Spike Trig";

    private void Start() {
        currentState = State.OPEN;
        position.x = transform.localPosition.x;
        isActive = true;
        GetComponent<SpriteRenderer>().sprite = spikes[Random.Range(0, spikes.Length)];
    }

    private void Update() {
        switch (currentState) {
            case State.OPEN_UP:
                if (position.y >= 0) {
                    currentState = State.OPEN;
                    position.y = 0;
                } else {
                    timeElasped = incrementTime(timeElasped);
                    position.y += smoothStop(timeElasped) * height;
                }
                transform.localPosition = position;
                break;

            case State.CLOSE_UP:
                if (position.y <= -height) {
                    currentState = State.CLOSE;
                    position.y = -height;
                } else {
                    timeElasped = incrementTime(timeElasped);
                    position.y -= smoothStop(timeElasped) * height;
                }
                transform.localPosition = position;
                break;

            case State.OPEN:
            case State.CLOSE:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag(SPIKE_TAG)) {
            currentState = State.CLOSE_UP;
            timeElasped = 0;
            isActive = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag(SPIKE_TAG)) {
            currentState = State.OPEN_UP;
            timeElasped = 0;
            isActive = true;
        }
    }

    public override void Collided() {
    }

    private float incrementTime(float time) {
        time += Time.deltaTime * speed;
        if (time > 1.0f) {
            time = 1.0f;
        }
        return time;
    }

    private float smoothStop(float t) {
        // instead of linear mapping we are gonna use smoothstop
        float temp = 1 - t;
        return (1 - (temp * temp * temp));
    }
}
