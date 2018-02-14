using UnityEngine;

public class Copter : Enemy, IAttackable {

    public float speed = 1.3f;

    void OnEnable() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
    }

    public void Attack(int difficultyLevel, Vector2 playerPosition, int platformLevel) {
        _transform.position = new Vector3(playerPosition.x, playerPosition.y + 8f, 0);
    }

}
