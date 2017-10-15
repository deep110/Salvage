using UnityEngine;

public class Tank : Enemy {

	void OnEnable() {
		_rigidbody.velocity = _velocity;
	}

	/**
	 * @param {boolean} right - indicating whether bee should start from left
	 *							or right
	 */
	public void Move(bool right) {
		var localPosition = new Vector3();
		Vector3 localScale = _transform.localScale;

		if (right) {
			_velocity.Set(0.5f, 0);
			localPosition.Set(-3f, 0.74f, 0);
			localScale.x = -1;
		} else {
			_velocity.Set(-0.5f, 0);
			localPosition.Set(3f, 0.74f, 0);
			localScale.x = 1;
		}

		_transform.position += localPosition;
		_transform.localScale = localScale;
	}
}
