using UnityEngine;

public class Ball : Enemy, IAttackable {

    public float speed = 1.35f;

    private Vector2 _velocity;

    private void OnEnable() {
        GetComponent<Rigidbody2D>().velocity = _velocity;
        GetComponent<Rigidbody2D>().AddTorque(1.4f);
    }

    public void Attack(int difficultyLevel, Vector2 playerPosition, int platformLevel) {
        float ballPositionY = playerPosition.y + platformLevel * PLATFORM_GAP + 1.3f;
        var localPosition = new Vector3(MAX_SCREEN_X, ballPositionY, 0);
        _velocity.x = getSpeed(difficultyLevel);

        if (playerPosition.x > 0) {
            localPosition.x *= -1;
        } else {
            _velocity.x *= -1;
        }

        _transform.position = localPosition;
    }

    private float getSpeed(int difficultyLevel) {
        return speed - difficultyLevel * (speed/10f);
    }
}
