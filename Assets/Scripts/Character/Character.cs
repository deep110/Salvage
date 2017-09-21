using UnityEngine;
using System;

public class Character : MonoBehaviour {

	public bool isFirstPlayer = true;

	// player controls
	public float maxSpeed = 1.7f;
	public float jumpForce = 27f;

	// private variables
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private Animator _animator;
	private Vector2 lastStablePosition;

	// hold player motion in this timestep
	private Vector2 velocity;

	// player tracking
	private bool facingRight = true;
	private bool isJumping;
	private bool isFalling;
	private bool canFall;

	// store the layer the player is on (setup in Awake)
	private int playerLayer;

	// layer of platform (setup in Awake)
	private int platformLayer;

	private int platformsClimbed;
	
	void Awake() {
		// get a reference to the components we are going to be changing and store
		// a reference for efficiency purposes
		_transform = GetComponent<Transform> ();
		_rigidbody = GetComponent<Rigidbody2D> ();
		_animator = GetComponent<Animator> ();

		playerLayer = gameObject.layer;
		platformLayer = LayerMask.NameToLayer("Platform");
		Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);

		lastStablePosition = new Vector2(_transform.position.x, _transform.position.y);
	}
		
	void Update() {
		
		velocity.y = _rigidbody.velocity.y;

		// Change the actual velocity on the rigidbody
		_rigidbody.velocity = velocity;

		if (isJumping && velocity.y < 0) {
			Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
			isJumping = false;
		}

		if (isFalling && velocity.y < -6) {
			Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
			isFalling = false;
		}
	}

	public void Move(float inputX) {
		velocity.x = Math.Sign(inputX) * maxSpeed;

		_animator.SetFloat("Speed", Mathf.Abs(velocity.x));
	}

	public void Jump() {
		if(!isJumping){
			isJumping = true;
			_animator.SetTrigger("Jump");

			Physics2D.IgnoreLayerCollision(playerLayer, platformLayer);
			// reset current vertical motion to 0 prior to jump
			velocity.y = 0f;
			// add a force in the up direction
			_rigidbody.AddForce (new Vector2 (0, 100*jumpForce));
			// play the jump sound
		}
	}

	public void Fall() {
		if (canFall && !isFalling) {
			isFalling = true;
			_animator.SetTrigger("Jump");

			Physics2D.IgnoreLayerCollision(playerLayer, platformLayer);
			// reset current vertical motion to 0 prior to jump
			velocity.y = 0f;
			// add a force in the down direction
			_rigidbody.AddForce (new Vector2 (0, -60*jumpForce));
		}
	}

	// Checking to see if the sprite should be flipped
	// this is done in LateUpdate since the Animator may override the localScale
	// this code will flip the player even if the animator is controlling scale
	void LateUpdate() {
		
		Vector3 localScale = _transform.localScale;

		// moving right so face right
		if (velocity.x > 0) {
			facingRight = true;
		} else facingRight &= !(velocity.x < 0); // moving left so face left

		// check to see if scale x is right for the player
		// if not, multiple by -1 which is an easy way to flip a sprite
		if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0))) {
			localScale.x *= -1;
		}
		// update the scale
		_transform.localScale = localScale;
	}


	void OnCollisionEnter2D(Collision2D other) {
		Transform colliderTransform = other.transform;

		if (colliderTransform.CompareTag ("Platform")) {
			canFall = true;
			if (isFirstPlayer) {
				lastStablePosition.y = colliderTransform.position.y;
				updatePlatformsClimbed(colliderTransform.GetComponent<Platform>().platformIndex);
			}
		} else if (colliderTransform.CompareTag ("Ground")) {
			if (isFirstPlayer) {
				lastStablePosition.y = colliderTransform.position.y;
			}
			canFall = false;
		}
	}

	public Vector2 GetLastStablePosition() {
		lastStablePosition.x = _transform.position.x;
		return lastStablePosition;
	}

	private void updatePlatformsClimbed(int platformIndex) {
		if (platformIndex > platformsClimbed) {
			platformsClimbed = platformIndex;
			EventManager.PlatformClimbed(platformsClimbed);
		}
	}
}
