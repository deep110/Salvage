using System.Collections;
using UnityEngine;

///<summary>
/// This script is for handling player collisions with interactables like
/// coins, powerups and enemies.
///</summary>
public class CharacterCollider : MonoBehaviour {

    public GameObject shockwave;
    public ParticleSystem playerDeath;

    [HideInInspector]
    public bool isShieldActive;

    private CameraShake cameraShake;
    private GameObject shield;
    private bool isInvincible;
    private bool isDead;

    private void Awake() {
        shield = transform.GetChild(0).gameObject;
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (!isDead) {
            if (other.CompareTag("Enemy")) {
                Enemy enemy = other.GetComponent<Enemy>();
                if (!isInvincible && enemy.isActive) {
                    enemy.Collided();
                    StartCoroutine(HandleEnemyCollision());
                }

            } else if (other.CompareTag("Crystal")) {
                other.gameObject.GetComponent<Crystal>().Fall();

            } else if (other.CompareTag("PowerUp")) {
                PowerUpManager.Instance.AddActivePowerUp(other.gameObject.GetComponent<PowerUp>());
            }
        }
    }

    public void ActivateShield() {
        isShieldActive = true;
        shield.SetActive(true);
    }

    public void DeActivateShield() {
        if (isShieldActive) {
            isShieldActive = false;
            StartCoroutine(BlinkShield(2f));
        } else {
            // player has collided with enemy
            // just sync shield state visually
            shield.SetActive(false);
        }
    }

    public void PlayerRevived() {
        isDead = false;
        StartCoroutine(BlinkPlayer(2f));
    }

    private IEnumerator HandleEnemyCollision() {
        cameraShake.Apply(0.5f);

        if (isShieldActive) {
            isShieldActive = false;
            StartCoroutine(BlinkPlayer(2f));
        } else {
            isDead = true;
            GetComponent<Character>().PlayerDeath();
            playerDeath.Play();
            Instantiate(shockwave, transform.position, Quaternion.identity);
            
            yield return new WaitForSeconds(1.2f);
            
            PlayerManager.Instance.PlayerCollided();
        }
    }

    private IEnumerator BlinkShield(float timer) {
        isInvincible = true;

        float time = 0;
        bool currentBlink = false;
        const float blinkPeriod = 0.15f;

        while (time < timer && isInvincible && !isShieldActive) {
            shield.SetActive(currentBlink);

            yield return new WaitForSeconds(blinkPeriod);
            time += blinkPeriod;

            currentBlink = !currentBlink;
        }

        shield.SetActive(isShieldActive); //TODO: animate shield disappearance
        isInvincible = false;
    }

    private IEnumerator BlinkPlayer(float timer) {
        isInvincible = true;

        float time = 0;
        bool currentBlink = false;
        const float blinkPeriod = 0.15f;

        while (time < timer && isInvincible) {
            gameObject.GetComponent<SpriteRenderer>().enabled = currentBlink;

            yield return new WaitForSeconds(blinkPeriod);
            time += blinkPeriod;

            currentBlink = !currentBlink;
        }

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        isInvincible = false;
    }

}