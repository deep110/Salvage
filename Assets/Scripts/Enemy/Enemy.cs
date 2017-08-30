using UnityEngine;

public abstract class Enemy : MonoBehaviour {

	public Vector2 _velocity;
	protected Rigidbody2D _rigidbody;

	protected virtual void Awake() {
		_rigidbody = GetComponent <Rigidbody2D>();
	}

	protected virtual void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("PlayerOne") || other.CompareTag("PlayerTwo")) {
			EventManager.GameOver();
			gameObject.SetActive(false);
		}
	}
}
