﻿using UnityEngine;

public class Coin : MonoBehaviour {

	public Vector2 fallVelocity = new Vector2(0, -3);

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
				GetComponent<Rigidbody2D>().velocity = fallVelocity;
				timePassed = Time.timeSinceLevelLoad;
			}
		}
		else if (Time.timeSinceLevelLoad - timePassed > 0.1f) {
			GetComponent<Transform> ().GetComponentInParent<Platform> ().SetCoinState (index);
			Collect();
		}
	}

	public void Collect() {
		EventManager.CoinCollected();
		gameObject.SetActive(false);
		isFalling = false;
		index = -1;
	}

	public void Teleport() {
		GetComponent<Transform>().GetComponentInParent<Platform>().SetCoinState(index);
		Collect();
	}

}
