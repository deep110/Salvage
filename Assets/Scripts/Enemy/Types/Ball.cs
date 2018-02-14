using UnityEngine;

public class Ball : Enemy, IAttackable {

    public float speed = 1.3f;

    private Vector2 _velocity = Vector2.zero;

    void OnEnable() {
        GetComponent<Rigidbody2D>().velocity = _velocity;
    }

    public void Attack(int difficultyLevel, Vector2 playerPosition, int platformLevel) {
        float ballPositionY = playerPosition.y + platformLevel * PLATFORM_GAP + 1.3f;
        var localPosition = new Vector3(MAX_SCREEN_X, ballPositionY, 0);
        Vector3 localScale = _transform.localScale;
        _velocity.x = speed;

        if (playerPosition.x > 0) {
            localPosition.x *= -1;
            localScale.x = -1;
        } else {
            localScale.x = 1;
            _velocity.x *= -1;
        }

        _transform.position = localPosition;
        _transform.localScale = localScale;
    }
}
