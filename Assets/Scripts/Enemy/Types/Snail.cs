using UnityEngine;

public class Snail : Enemy, IAttackable {

    public float speedMin = 0.3f;
    public float speedMax = 0.5f;

    private bool isMovingRight;
    private Rigidbody2D _rigidbody;
    private float timeElasped;

    protected override void Awake() {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        timeElasped += Time.fixedDeltaTime;
        float velX = speedMin + (speedMax - speedMin) * Mathf.Sin(1.5f * timeElasped);

        if (!isMovingRight) velX *= -1f;

        _rigidbody.velocity = new Vector2(velX, 0);
    }

    public void Attack(int difficultyLevel, Vector2 playerPosition, int platformLevel) {
        float tankPositionY = playerPosition.y + platformLevel * PLATFORM_GAP + 0.7f;
        var localPosition = new Vector3(MAX_SCREEN_X, tankPositionY, 0);
        Vector3 localScale = _transform.localScale;
        timeElasped = 0;

        if (playerPosition.x > 0) {
            localPosition.x *= -1;
            localScale.x = -1;
            isMovingRight = true;
        } else {
            localScale.x = 1;
            isMovingRight = false;
        }

        _transform.position = localPosition;
        _transform.localScale = localScale;
    }
}
