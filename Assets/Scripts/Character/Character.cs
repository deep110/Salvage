using UnityEngine;

public class Character : MonoBehaviour {

	public bool isFirstPlayer = true;

	// player controls
	public float horiForce = 25f;
	public float jumpForce = 43f;
	public float hoverHeight = 0.2f;
	public float hoverForce = 60f;

	public Transform rayCastPosition;
	public bool IsJumping { get {return isJumping;} }

	// private variables
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private Animator _animator;
	private CapsuleCollider2D _collider;

	// player tracking
	private bool isJumping;
	private float forceX;
	private bool hasEnteredPlatform;
	private Vector2 lastStablePosition;

	private const int platformLayerMask = 1 << 10;
	private int platformsClimbed;
	
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

		lastStablePosition = new Vector2(_transform.position.x, rayCastPosition.position.y - hoverHeight);
	}

	public void Move(float inputX) {
		// scale the inputs correctly
		if (isFirstPlayer) {
			forceX = inputX * 1080f;
		} else {
			forceX = inputX * 35f;
		}

		// apply force to move
		if (!Mathf.Approximately(forceX, 0)) {
			forceX = Mathf.Sign(forceX) * Mathf.Clamp(Mathf.Abs(forceX), 5f, 15f) * horiForce;
			_rigidbody.AddRelativeForce(new Vector2(forceX, 0));
		}

		// update animation from idle -> run
		_animator.SetFloat("Speed", Mathf.Abs(_rigidbody.velocity.x));
	}

	public void Jump() {
		if(!isJumping) {
			isJumping = true;
			_animator.SetTrigger("Jump");
			_collider.isTrigger = true;
			// add a force in the up direction
			_rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			// play the jump sound
		}
	}

	public void Hover() {
		if (!isJumping) {
			RaycastHit2D hit = Physics2D.Raycast(rayCastPosition.position, -Vector2.up, hoverHeight,
				platformLayerMask);

	        if (hit.collider != null) {
	            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
	            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce * _rigidbody.mass;
	            _rigidbody.AddForce(appliedHoverForce);
	        }
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		hasEnteredPlatform |= (other.CompareTag("Platform") && isJumping);
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag("Platform") && isJumping && hasEnteredPlatform) {
			_collider.isTrigger = false;
			hasEnteredPlatform = false;
			if (isFirstPlayer) {
				lastStablePosition.y = other.transform.position.y;
				updatePlatformsClimbed();
			}

			// reset jump after sometime
			Invoke("allowJump", 0.3f);
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

	private void allowJump() {
		isJumping = false;
	}
}
