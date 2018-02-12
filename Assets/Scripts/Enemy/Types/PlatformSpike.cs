using UnityEngine;

public class PlatformSpike : Enemy {

    public float height = 0.3f;
    public float speed = 0.7f;

    private enum State {
        CLOSE, CLOSE_UP,
        OPEN, OPEN_UP
    };

    private State currentState;
    private Vector2 position = Vector2.zero;
    private float timeElasped;

    void Start() {
        currentState = State.OPEN;
        position.x = transform.localPosition.x;
        isActive = true;
    }

    void Update() {
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

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Spike Trig")) {
            currentState = State.CLOSE_UP;
            timeElasped = 0;
            isActive = false;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.CompareTag("Spike Trig")) {
            currentState = State.OPEN_UP;
            timeElasped = 0;
            isActive = true;
        }
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
