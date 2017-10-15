using UnityEngine;

public abstract class Enemy : MonoBehaviour {

	public Vector2 _velocity;
	protected Rigidbody2D _rigidbody;
	protected Transform _transform;

	protected virtual void Awake() {
		_rigidbody = GetComponent <Rigidbody2D>();
		_transform = GetComponent<Transform>();
	}

	public virtual void Collided() {
		// play enemy die animation
		// play sound
		gameObject.SetActive(false);
	}
}
