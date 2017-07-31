using UnityEngine;

public abstract class Enemy : MonoBehaviour {

	// void Start () {
		
	// }
	
	// void Update () {
		
	// }

	protected void OnTriggerEnter2D(Collider2D other) {
		if (other.transform.CompareTag("PlayerOne") || other.transform.CompareTag("PlayerTwo")) {
			EventManager.GameOver();
		}
	}
}
