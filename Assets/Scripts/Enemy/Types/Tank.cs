using UnityEngine;

public class Tank : Enemy, IAttackable {

    public float speed = 0.5f;

    private Vector2 _velocity;

    private void OnEnable() {
        GetComponent<Rigidbody2D>().velocity = _velocity;
    }

    public void Attack(int difficultyLevel, Vector2 playerPosition, int platformLevel) {
        float tankPositionY = playerPosition.y + platformLevel * PLATFORM_GAP + 0.74f;
        var localPosition = new Vector3(MAX_SCREEN_X, tankPositionY, 0);
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
