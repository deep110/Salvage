using UnityEngine;

public class Book : Enemy {

	void OnEnable() {
		_velocity.Set(0, -1.3f);
		_rigidbody.velocity = _velocity;
	}

	void FixedUpdate() {
		_rigidbody.rotation += 1.2f;
	}
}
