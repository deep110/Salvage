using UnityEngine;

///<summary>
/// This script is for handling player collisions with interactables like
/// coins, powerups and enemies.
///</summary>
public class CharacterCollider : MonoBehaviour {

	// private GameObject shield;

	private SpriteRenderer _spriteRenderer;

	void Awake() {
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Enemy")) {
			other.gameObject.GetComponent<Enemy>().Collided();
		} else if (other.CompareTag("Coin")) {
			other.gameObject.GetComponent<Coin>().Fall();
		} else if (other.CompareTag("PowerUp")) {
			PowerUpManager.Instance.AddActivePowerUp(other.gameObject.GetComponent<PowerUp>());
		}
	}

	// private void ActivateShield() {
	// 	shield.SetActive(true);
	// }

}