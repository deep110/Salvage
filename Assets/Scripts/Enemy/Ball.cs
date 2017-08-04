using UnityEngine;

public class Ball : Enemy {

	private Rigidbody2D _rigidbody;

	void Start () {
		_rigidbody = transform.GetComponent<Rigidbody2D>();
		_rigidbody.velocity = new Vector2(1.2f, 0);
	}
}
