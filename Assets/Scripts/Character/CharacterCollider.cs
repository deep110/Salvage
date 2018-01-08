using System.Collections;
using UnityEngine;

///<summary>
/// This script is for handling player collisions with interactables like
/// coins, powerups and enemies.
///</summary>
public class CharacterCollider : MonoBehaviour {

	[HideInInspector]
	public bool deActivateShield;

	private CameraShake cameraShake;
	private GameObject shield;
	private bool isInvincible;

	void Awake() {
		shield = transform.GetChild(0).gameObject;
		cameraShake = Camera.main.GetComponent<CameraShake>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Enemy")) {

			other.gameObject.GetComponent<Enemy>().Collided();
			StartCoroutine(HandleEnemyCollision());

		} else if (other.CompareTag("Egg")) {
			other.gameObject.GetComponent<Egg>().Fall();

		} else if (other.CompareTag("PowerUp")) {
			PowerUpManager.Instance.AddActivePowerUp(other.gameObject.GetComponent<PowerUp>());
		}
	}

	public void ActivateShield() {
		isInvincible = true;
		shield.SetActive(true);
	}

	public void DeActivateShield() {
		isInvincible = false;
		deActivateShield = false;
		shield.SetActive(false);

		//TODO: blink the shield
	}

	private IEnumerator HandleEnemyCollision() {
		cameraShake.Apply(0.5f);
		yield return new WaitForSeconds(0.7f);

		if (isInvincible) {
			deActivateShield = true;
		} else {
			EventManager.GameOver();
		}
	}

}