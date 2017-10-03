using UnityEngine;

///<summary>
/// This script is for handling player collisions with interactables like
/// coins, powerups and enemies.
///</summary>
public class CharacterCollider : MonoBehaviour {

	[HideInInspector]
	public bool deActivateShield;

	private GameObject shield;
	private bool isShieldActive;

	void Awake() {
		shield = transform.GetChild(0).gameObject;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Enemy")) {
			other.gameObject.GetComponent<Enemy>().Collided();
			print ("Collided");
			if (isShieldActive) {
				deActivateShield = true;
			} else {
				EventManager.GameOver();
			}
		} else if (other.CompareTag("Coin")) {
			other.gameObject.GetComponent<Coin>().Fall();

		} else if (other.CompareTag("PowerUp")) {
			PowerUpManager.Instance.AddActivePowerUp(other.gameObject.GetComponent<PowerUp>());
		}
	}

	public void ActivateShield() {
		isShieldActive = true;
		shield.SetActive(true);
	}

	public void DeActivateShield() {
		isShieldActive = false;
		deActivateShield = false;
		shield.SetActive(false);

		//TODO: blink the shield
	}

}