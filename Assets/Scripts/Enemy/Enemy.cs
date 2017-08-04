using UnityEngine;

public abstract class Enemy : MonoBehaviour {

	protected void OnTriggerEnter2D(Collider2D other) {
		sendGameOverEvent(other.transform);
	}

	protected void OnCollisionEnter2D(Collision2D other) {
		sendGameOverEvent(other.transform);
	}

	private void sendGameOverEvent(Transform other) {
		if (other.CompareTag("PlayerOne") || other.CompareTag("PlayerTwo")) {
			EventManager.GameOver();
		}
	}
}
