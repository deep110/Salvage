using UnityEngine;

public class Coin : MonoBehaviour {

	public float fallSpeed = 3f;

	private Rigidbody2D _rigidbody;
	private bool isFalling;
	
	void Awake () {
		_rigidbody = GetComponent<Rigidbody2D> ();
	}

	public void Fall() {
		isFalling = true;
		_rigidbody.velocity = new Vector2(0, -1 * fallSpeed);
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.transform.CompareTag("PlayerOne")) {
			Fall();
		} else if (other.transform.CompareTag("PlayerTwo")){
			if (!isFalling) {
				Fall();
			} else {
				isFalling = false;
				gameObject.SetActive(false);
			}
		}
	}
}
