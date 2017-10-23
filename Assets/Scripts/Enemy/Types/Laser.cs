using UnityEngine;

public class Laser : Enemy {

	public enum State {
		ON, WARM_UP, OFF
	};

	public GameObject line, warmup;
	public float ontime = 1f, total = 2f;

	private State current;

	protected override void Awake() {
		current = State.OFF;
		line.SetActive(false);
		warmup.SetActive(false);
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
