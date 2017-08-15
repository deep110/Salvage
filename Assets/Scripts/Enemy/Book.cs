using UnityEngine;

public class Book : Enemy {

	public Vector2 velocity = new Vector2(0, -1.3f);
	private Rigidbody2D _rigidbody;

	void OnEnable() {
		_rigidbody = GetComponent<Rigidbody2D>();
		_rigidbody.velocity = velocity;
	}

	void FixedUpdate() {
		_rigidbody.rotation += 1.2f;
	}
}
