﻿using UnityEngine;

public class PlayerController : MonoBehaviour {

	// player controls
	public float maxSpeed = 2f;
	public float jumpForce = 100f;
	public bool isAlive = true;

	// LayerMask to determine what is considered ground for the player
	public LayerMask whatIsGround;

	// Transform just below feet for checking if player is grounded
	public Transform groundCheck;

	// store references to components on the gameObject
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private Animator _animator;

	// hold player motion in this timestep
	private float vx;
	private float vy;

	// player tracking
	bool facingRight = true;
	bool isGrounded = true;
	bool isJumping;
	bool isFalling;

	bool canFall = true;

	// store the layer the player is on (setup in Awake)
	private int playerLayer;

	// layer of platform(setup in Awake)
	private int platformLayer;
	
	void Awake () {
		// get a reference to the components we are going to be changing and store a reference for efficiency purposes
		_transform = GetComponent<Transform> ();
		
		_rigidbody = GetComponent<Rigidbody2D> ();

		_animator = GetComponent<Animator> ();

		playerLayer = gameObject.layer;
		platformLayer = LayerMask.NameToLayer("Platform");
	}
		
	void Update(){
		
		// exit update if player cannot move or game is paused
		//if (!playerCanMove || (Time.timeScale == 0f))
		//	return;
		vy = _rigidbody.velocity.y;

		// Change the actual velocity on the rigidbody
		_rigidbody.velocity = new Vector2(vx, vy);

		// Check to see if character is grounded by raycasting from the middle of the player
		// down to the groundCheck position and see if collected with gameobjects on the
		// whatIsGround layer
		isGrounded = Physics2D.Linecast(_transform.position, groundCheck.position, whatIsGround);

		if (isJumping && vy < 0){
			Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
			isJumping = false;
		}

		if (isFalling && vy < -4) {
			Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
			isFalling = false;
		}

	}

	public void Move(float inputX){
		int deltaX = 0;
		if (inputX > _transform.position.x + 0.2f) {
			deltaX = 1;
		} else if (inputX + 0.2f < _transform.position.x) deltaX = -1;
		vx = deltaX * maxSpeed;

		_animator.SetFloat("Speed", Mathf.Abs(vx));
	}

	public void Jump() {
		if(isGrounded && !isJumping){
			isJumping = true;
			
			_animator.SetTrigger("Jump");
			Physics2D.IgnoreLayerCollision(playerLayer, platformLayer);
			// reset current vertical motion to 0 prior to jump
			vy = 0f;
			// add a force in the up direction
			_rigidbody.AddForce (new Vector2 (0, 100*jumpForce));
			// play the jump sound
		}
	}

	public void Fall() {
		if(isGrounded && canFall && !isFalling){
			isFalling = true;
			_animator.SetTrigger("Jump");
			Physics2D.IgnoreLayerCollision(playerLayer, platformLayer);
			// reset current vertical motion to 0 prior to jump
			vy = 0f;
			// add a force in the down direction
			_rigidbody.AddForce (new Vector2 (0, -1*jumpForce));
		}
	}

	// Checking to see if the sprite should be flipped
	// this is done in LateUpdate since the Animator may override the localScale
	// this code will flip the player even if the animator is controlling scale
	void LateUpdate() {
		
		Vector3 localScale = _transform.localScale;

		// moving right so face right
		if (vx > 0){
			facingRight = true;
		} else if (vx < 0) { // moving left so face left
			facingRight = false;
		}

		// check to see if scale x is right for the player
		// if not, multiple by -1 which is an easy way to flip a sprite
		if (((facingRight) && (localScale.x<0)) || ((!facingRight) && (localScale.x>0))) {
			localScale.x *= -1;
		}
		// update the scale
		_transform.localScale = localScale;
	}


	void OnCollisionEnter2D(Collision2D other){
		canFall = other.transform.tag.Equals ("Platform");
	}
}
