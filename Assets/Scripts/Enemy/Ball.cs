using UnityEngine;

public class Ball : Enemy {

	private Rigidbody2D _rigidbody;

	// Use this for initialization
	void Start () {
		_rigidbody = transform.GetComponent<Rigidbody2D>();
		_rigidbody.velocity = new Vector2(1.2f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
