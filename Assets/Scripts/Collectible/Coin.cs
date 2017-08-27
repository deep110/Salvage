using UnityEngine;

public class Coin : MonoBehaviour {

	public float fallSpeed = 3f;

	private Rigidbody2D _rigidbody;
	private bool hasFallen;
	private int index = -1;
	
	void Awake() {
		_rigidbody = GetComponent<Rigidbody2D> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.transform.CompareTag("PlayerOne")) {
			Fall();
		} else if (other.transform.CompareTag("PlayerTwo")){
			if (!hasFallen) {
				Fall();
			} else {
				EventManager.CoinCollected();
				gameObject.SetActive(false);
				hasFallen = false;
			}
		}
	}

	/**
	* called by Platform to set Index
	* so that we can keep track of coins.
	*/
	public void SetIndex(int index) {
		this.index = index;
	}

	private void Fall() {
		hasFallen = true;
		_rigidbody.velocity = new Vector2(0, -1 * fallSpeed);
		if (index != -1) {
			transform.parent.GetComponent<Platform>().SetCoinState(index);
			index = -1;
		}
	}

}
