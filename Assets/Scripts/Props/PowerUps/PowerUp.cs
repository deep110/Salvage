using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PowerUp : MonoBehaviour {

	public string powerUpName;
	public float duration;
	public float fallSpeed = 1.6f;

	public bool IsActive { get { return active; } }

	protected float timeSinceStart;
	protected bool active;

	protected virtual void Start() {
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * fallSpeed);
	}

	/// powerup has been collected
	/// starts powerup life time.
	public virtual void Collected() {
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		timeSinceStart = 0;
		active = true;
	}

	/// called when powerup is running
	/// by default it does nothing, override to do per frame manipulation
	public virtual void Tick() {
		timeSinceStart += Time.deltaTime;
		if (timeSinceStart >= duration) {
			active = false;
            return;
        }
	}

	/// call to end powerup
	public virtual void Ended() {
		Destroy(gameObject);
	}

	public void ResetTime() {
		timeSinceStart = 0;
	}
}
