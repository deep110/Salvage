using UnityEngine;

public abstract class Enemy : MonoBehaviour {

	protected Transform _transform;

	protected virtual void Awake() {
		_transform = GetComponent<Transform>();
	}

	public virtual void Collided() {
		// play enemy die animation
		// play sound
		gameObject.SetActive(false);
	}
}
