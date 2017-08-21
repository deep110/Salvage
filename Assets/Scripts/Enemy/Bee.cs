using UnityEngine;

public class Bee : Enemy {

	private Vector2 velocity;

	void OnEnable() {
		GetComponent<Rigidbody2D>().velocity = velocity;	
	}

	/**
	 * @param {boolean} right - indicating whether bee should start from left
	 *							or right
	 */
	public void Fly(bool right) {
		var localPosition = new Vector3();
		if (right) {
			velocity.Set(0.7f, 0);
			localPosition.Set(-3f, 0.5f, 0);
		} else {
			velocity.Set(-0.7f, 0);
			localPosition.Set(3f, 0.5f, 0);
		}

		GetComponent<Transform>().position += localPosition;
	}
}
