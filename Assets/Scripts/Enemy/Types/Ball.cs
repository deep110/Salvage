using UnityEngine;

public class Ball : Enemy, IAttackable {

    public float speed = 1.35f;

    private Rigidbody2D _rigidbody;
    private Vector2 velocity;

    protected override void Awake() {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Attack(int difficultyLevel, Vector2 playerPosition, int platformLevel) {
        float ballPositionY = playerPosition.y + platformLevel * PLATFORM_GAP + 1.3f;
        var localPosition = new Vector3(MAX_SCREEN_X, ballPositionY, 0);
        velocity.x = getSpeed(difficultyLevel);

        if (playerPosition.x > 0) {
            localPosition.x *= -1;
        } else {
            velocity.x *= -1;
        }

        _transform.position = localPosition;

        _rigidbody.velocity = velocity;
        _rigidbody.AddTorque(1.4f);
    }

    private float getSpeed(int difficultyLevel) {
        return speed - difficultyLevel * (speed/10f);
    }
}
