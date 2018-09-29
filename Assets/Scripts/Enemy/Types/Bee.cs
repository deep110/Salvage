using UnityEngine;

public class Bee : Enemy, IAttackable {

    public float speed = 1.35f;

    public void Attack(int difficultyLevel, Vector2 playerPosition, int platformLevel) {
        _transform.position = new Vector3(playerPosition.x, playerPosition.y + 8f, 0);

        GetComponent<Rigidbody2D>().velocity = new Vector2(
            0,
            -speed + difficultyLevel * (speed/10f)
        );
    }

}
