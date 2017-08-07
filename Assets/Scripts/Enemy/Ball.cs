using UnityEngine;

public class Ball : Enemy {

	private Rigidbody2D _rigidbody;

	void Start() {
		_rigidbody = transform.GetComponent<Rigidbody2D>();
	}

	public void Roll(bool right) {
		_rigidbody.velocity = (right) ? new Vector2(1.2f, 0) : new Vector2(-1.2f, 0);
	}
}
