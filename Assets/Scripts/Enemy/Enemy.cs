using UnityEngine;

public abstract class Enemy : MonoBehaviour {

	protected void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("PlayerOne") || other.CompareTag("PlayerTwo")) {
			EventManager.GameOver();
			gameObject.SetActive(false);
		}
	}
}
