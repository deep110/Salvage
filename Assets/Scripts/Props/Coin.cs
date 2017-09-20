using UnityEngine;

public class Coin : MonoBehaviour {

	public float fallSpeed = 3f;

	private int index = -1;
	private bool isFalling;
	private float timePassed;

	/// called by Platform to set Index
	/// so that we can keep track of coins.
	public void SetIndex(int index) {
		this.index = index;
		isFalling = false;
	}

	public void Fall() {
		if (!isFalling) {
			if (index != -1) {
				isFalling = true;
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * fallSpeed);
				GetComponent<Transform>().GetComponentInParent<Platform>().SetCoinState(index);
				index = -1;
				timePassed = Time.timeSinceLevelLoad;
			}
		} else if (Time.timeSinceLevelLoad - timePassed > 0.1f) {
			Collect();
		}
	}

	public void Collect() {
		EventManager.CoinCollected();
		gameObject.SetActive(false);
		isFalling = false;
	}

}
