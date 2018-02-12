using UnityEngine;

public class Ball : Enemy {

    public Vector2 _velocity;

    void OnEnable() {
        GetComponent<Rigidbody2D>().velocity = _velocity;
    }

    /**
	 * @param {boolean} right - indicating whether ball should start from left
	 *							or right
	 */
    public void Roll(bool right) {
        var localPosition = new Vector3();
        Vector3 localScale = _transform.localScale;

        if (right) {
            _velocity.Set(1.3f, 0);
            localPosition.Set(-3f, 1.3f, 0);
            localScale.x = -1;
        } else {
            _velocity.Set(-1.3f, 0);
            localPosition.Set(3f, 1.3f, 0);
            localScale.x = 1;
        }

        _transform.position += localPosition;
        _transform.localScale = localScale;
    }
}
