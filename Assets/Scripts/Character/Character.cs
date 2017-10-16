using UnityEngine;
using System;

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

	// hold player motion in this timestep
	private Vector2 velocity;

	// player tracking
	private bool facingRight = true;
	private bool isJumping;
	private bool isFalling;
	private bool canFall;

	private int platformsClimbed;

	private bool hasEnteredPlatform;
	
	void Awake() {
		// get a reference to the components we are going to be changing and store
		// a reference for efficiency purposes
		_transform = GetComponent<Transform> ();
		_rigidbody = GetComponent<Rigidbody2D> ();
		_animator = GetComponent<Animator> ();
		_collider = GetComponent<CapsuleCollider2D>();

		lastStablePosition = new Vector2(_transform.position.x, _transform.position.y);

		if (isFirstPlayer) {
			Vector3 position = _transform.position;
			position.y += 1.8f;
			_transform.position = position;
		}
	}

	public void Move(float inputX) {
		_rigidbody.AddForce (new Vector2 (Math.Sign (inputX) * horiForce, 0));
		_rigidbody.AddForce (new Vector2 (-drag * _rigidbody.velocity.x, 0));
		if (Mathf.Abs(_rigidbody.velocity.x) >= 0.01f)
			_animator.SetFloat ("Speed", Mathf.Abs (_rigidbody.velocity.x));
		else {
			_animator.SetFloat ("Speed", 0);
		}
	}

	public void Jump() {
		if(!isJumping && !isFalling){
			isJumping = true;
			isFalling = false;
			_animator.SetTrigger("Jump");
			_collider.isTrigger = true;
			// reset current vertical motion to 0 prior to jump
			velocity.y = 0f;
			// add a force in the up direction
			_rigidbody.AddForce (new Vector2 (0, 100*jumpForce));
			// play the jump sound
		}
	}

	public void Fall() {
		if (canFall && !isFalling && !isJumping) {
			isFalling = true;
			isJumping = false;
			_animator.SetTrigger("Jump");

			print ("Fall called");
			_collider.isTrigger = true;

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

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Platform") && isJumping) {
			hasEnteredPlatform = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Platform")) {
			if (isJumping) {
				if (hasEnteredPlatform) {
					_collider.isTrigger = false;
					hasEnteredPlatform = false;
				}
			} else if (isFalling) {
				_collider.isTrigger = false;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		Transform colliderTransform = other.transform;

		if (colliderTransform.CompareTag ("Platform")) {
			canFall = true;
			isJumping = false;
			isFalling = false;
			if (isFirstPlayer) {
				lastStablePosition.y = colliderTransform.position.y;
				updatePlatformsClimbed(colliderTransform.GetComponent<Platform>().platformIndex);
			}
		} else if (colliderTransform.CompareTag ("Ground")) {
			if (isFirstPlayer) {
				lastStablePosition.y = colliderTransform.position.y;
			}
			isJumping = false;
			isFalling = false;
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
