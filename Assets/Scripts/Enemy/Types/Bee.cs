using UnityEngine;

public class Bee : Enemy {

	void OnEnable() {
		_rigidbody.velocity = _velocity;
	}

	/**
	 * @param {boolean} right - indicating whether bee should start from left
	 *							or right
	 */
	public void Fly(bool right) {
		var localPosition = new Vector3();
		if (right) {
			_velocity.Set(0.7f, 0);
			localPosition.Set(-3f, 0.6f, 0);
		} else {
			_velocity.Set(-0.7f, 0);
			localPosition.Set(3f, 0.6f, 0);
		}

		GetComponent<Transform>().position += localPosition;
	}
}
