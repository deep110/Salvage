using UnityEngine;

public class Character : MonoBehaviour {

	public bool isFirstPlayer = true;

	// player controls
	public float drag = 20f;
	public float horiForce = 2f;
	public float jumpForce = 54f;

	// private variables
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private Animator _animator;
	private CapsuleCollider2D _collider;
	private Vector2 lastStablePosition;

	// player tracking
	private bool isJumping;

	private int platformsClimbed;
	private bool hasEnteredPlatform;
	
	void Awake() {
		// get a reference to the components we are going to be changing and store
		// a reference for efficiency purposes
		_transform = GetComponent<Transform>();
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
		_collider = GetComponent<CapsuleCollider2D>();

		if (isFirstPlayer) {
			Vector3 position = _transform.position;
			position.y += 1.8f;
			_transform.position = position;
		}

		lastStablePosition = new Vector2(_transform.position.x, _transform.position.y);
	}

	public void Move(float inputX) {
		float forceX = inputX * horiForce - drag * _rigidbody.velocity.x;
		_rigidbody.AddForce(new Vector2(forceX, 0));

		if (Mathf.Abs(_rigidbody.velocity.x) >= 0.01f) {
			_animator.SetFloat("Speed", Mathf.Abs(_rigidbody.velocity.x));
		}
		else {
			_animator.SetFloat("Speed", 0);
		}
	}

	public void Jump() {
		if(!isJumping){
			isJumping = true;
			_animator.SetTrigger("Jump");
			_collider.isTrigger = true;
			// add a force in the up direction
			_rigidbody.AddForce(new Vector2(0, 100*jumpForce));
			// play the jump sound
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		hasEnteredPlatform |= (other.CompareTag("Platform") && isJumping);
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag("Platform") && isJumping && hasEnteredPlatform) {
			_collider.isTrigger = false;
			hasEnteredPlatform = false;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		Transform colliderTransform = other.transform;

		if (colliderTransform.CompareTag("Platform") || colliderTransform.CompareTag("Ground")) {
			isJumping = false;
			if (isFirstPlayer) {
				lastStablePosition.y = colliderTransform.position.y;
				// since first player will never be on ground
				// we can safely assume it will be called by platform
				updatePlatformsClimbed();
			}
		}
	}

	public Vector2 GetLastStablePosition() {
		lastStablePosition.x = _transform.position.x;
		return lastStablePosition;
	}

	private void updatePlatformsClimbed() {
		platformsClimbed += 1;
		EventManager.PlatformClimbed(platformsClimbed);
	}
}
