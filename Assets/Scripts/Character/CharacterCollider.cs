using UnityEngine;

///<summary>
/// This script is for handling player collisions with interactables like
/// coins, powerups and enemies.
///</summary>
public class CharacterCollider : MonoBehaviour {

	// private GameObject shield;

	// private Renderer _renderer;

	// void Awake() {
	// 	_renderer = GetComponent<Renderer>();
	// }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Enemy")) {
			EventManager.GameOver();
			other.gameObject.GetComponent<Enemy>().Collided();
		} else if (other.CompareTag("Coin")) {
			other.gameObject.GetComponent<Coin>().Fall();
		}
	}

	// private void ActivateShield() {
	// 	shield.SetActive(true);
	// }

}