using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	public enum State {
		ON, WARM_UP, OFF
	};

	public float ontime = 1f, total = 2f;

	private State current;
	public GameObject line, warmup;

	void Start () {
		current = State.OFF;
		line.SetActive (false);
		warmup.SetActive (false);
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.L)) {
			Activate ();
		}
	}

	public void Activate () {
		WarmUp ();
		Invoke ("TurnOn", ontime);
		Invoke ("TurnOff", total);
	}

	private void WarmUp() {
		warmup.SetActive (true);
		current = State.WARM_UP;
	}

	private void TurnOn() {
		warmup.SetActive (false);
		line.SetActive (true);
		current = State.ON;
	}

	private void TurnOff() {
		line.SetActive (false);
	}

}
