using System.Collections;
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
		StartCoroutine(manageLaser());
	}

	private IEnumerator manageLaser() {
		// activate warmup
		warmup.SetActive (true);
		current = State.WARM_UP;

		yield return new WaitForSeconds(ontime);

		// turn off warmup and activate laser
		warmup.SetActive(false);
		line.SetActive(true);
		current = State.ON;

		yield return new WaitForSeconds(total - ontime);

		// deactivate laser
		line.SetActive (false);
		current = State.OFF;
	}

}
