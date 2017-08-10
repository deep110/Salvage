using UnityEngine;

public class Ball : Enemy {

	private Vector2 velocity;
	private Vector3 localPosition;

	void OnEnable() {
		GetComponent<Rigidbody2D>().velocity = velocity;	
	}

	/**
	 * @param {boolean} right - indicating whether ball should start from left
	 *							or right
	 */
	public void Roll(bool right) {
		if (right) {
			velocity.Set(1.2f, 0);
			localPosition.Set(-3f, 1f, 0);
		} else {
			velocity.Set(-1.2f, 0);
			localPosition.Set(3f, 1f, 0);
		}

		GetComponent<Transform>().position += localPosition;
	}
}
