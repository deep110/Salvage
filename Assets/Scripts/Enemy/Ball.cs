using UnityEngine;

public class Ball : Enemy {

	void OnEnable() {
		_rigidbody.velocity = _velocity;
	}

	/**
	 * @param {boolean} right - indicating whether ball should start from left
	 *							or right
	 */
	public void Roll(bool right) {
		var localPosition = new Vector3();
		if (right) {
			_velocity.Set(1.2f, 0);
			localPosition.Set(-3f, 1f, 0);
		} else {
			_velocity.Set(-1.2f, 0);
			localPosition.Set(3f, 1f, 0);
		}

		GetComponent<Transform>().position += localPosition;
	}
}
