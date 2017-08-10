using UnityEngine;

public abstract class Enemy : MonoBehaviour {

	protected void OnTriggerEnter2D(Collider2D other) {
		sendGameOverEvent(other.gameObject);
	}

	protected void OnCollisionEnter2D(Collision2D other) {
		sendGameOverEvent(other.gameObject);
	}

	private void sendGameOverEvent(GameObject other) {
		if (other.CompareTag("PlayerOne") || other.CompareTag("PlayerTwo")) {
			EventManager.GameOver();
			gameObject.SetActive(false);
		}
	}
}
