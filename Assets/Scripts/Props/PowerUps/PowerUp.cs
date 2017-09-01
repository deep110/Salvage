using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PowerUp : MonoBehaviour {

	public string name;
	public float duration;

	public bool Active { get { return _active; } }

	protected float fallSpeed = 2f;
	protected float timeSinceStart;
	protected bool _active;

	protected virtual void Awake() {
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * fallSpeed);
	}


	/// powerup has been collected
	public virtual void Collected() {
		
	}

	/// call to start powerup
	public virtual void Started() {
		timeSinceStart = 0;
	}

	/// called when powerup is running
	/// by default it does nothing, override to do per frame manipulation
	public virtual void Tick() {
		timeSinceStart += Time.deltaTime;
        if (timeSinceStart >= duration) {
            return;
        }
	}

	/// call to end powerup
	public virtual void Ended() {
		// clean up the resources
		Destroy(gameObject);
	}

	public void ResetTime() {
		timeSinceStart = 0;
	}
}
