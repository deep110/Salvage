using UnityEngine;

public class LaserManager : MonoBehaviour {

	private enum LaserSetState {
		HIDDEN, TO_BE_SEEN, SEEN, TO_BE_HIDDEN
	};

	public float moveDistance = 1f;
	public const int NumberOfLasers = 5;

	private float timeElapsed;
	private float[,] pattern = {
		{0.0f, 1.5f, 3.0f, 4.5f, 6.0f},
		{0.0f, 0.0f, 0.0f, 2.0f, 2.0f},
		{0.0f, 2.0f, 0.0f, 2.0f, 0.0f},
	};

	private Laser[] lasers;
	private LaserSetState current;

	private Vector3 hiddenPosition;
	private Vector3 seenPosition;

	void Awake() {
		lasers = new Laser[NumberOfLasers];
		//Initialize lasers
		for (int i = 0; i < NumberOfLasers; i++) {
			lasers [i] = transform.GetChild(i).GetComponent<Laser>();
		}

		current = LaserSetState.HIDDEN;
		hiddenPosition = transform.localPosition;
		seenPosition = hiddenPosition - new Vector3(0, moveDistance, 0);
	}

	void Update() {
		switch (current) {
			case LaserSetState.TO_BE_SEEN:
				transform.localPosition = Vector3.Lerp(hiddenPosition, seenPosition, timeElapsed);
				timeElapsed += Time.deltaTime;
				if (timeElapsed >= 1) {
					current = LaserSetState.SEEN;
					timeElapsed = 0;
				}
				break;
			case LaserSetState.TO_BE_HIDDEN:
				transform.localPosition = Vector3.Lerp(seenPosition, hiddenPosition, timeElapsed);
				timeElapsed += Time.deltaTime;
				if (timeElapsed >= 1) {
					current = LaserSetState.HIDDEN;
					timeElapsed = 0;
				}
				break;
			case LaserSetState.HIDDEN:
			case LaserSetState.SEEN:
				break;
		}
	}

	public float Activate() {
		//The first laser starts initialDelay seconds after it is enabled
		const float initialDelay = 2f;
		timeElapsed = 0;
		current = LaserSetState.TO_BE_SEEN;
		int index = Random.Range(0, pattern.Length / NumberOfLasers);
		float duration = 0;
		for (int i = 0; i < NumberOfLasers; i++) {
			if (pattern [index, i] > duration) {
				duration = pattern [index, i];
			}
			Invoke("ActivateLaser" + i, pattern [index, i] + initialDelay);
		}
		//for last laser will turn on till 2 sec then turn off + 1 sec wait before lasers disappear
		duration += 3;
		duration += initialDelay;
		Invoke("DeactivateLaser", duration);
		return duration;
	}

	private void ActivateLaser0() {
		lasers [0].Activate ();
	}

	private void ActivateLaser1() {
		lasers [1].Activate ();
	}
	private void ActivateLaser2() {
		lasers [2].Activate ();
	}
	private void ActivateLaser3() {
		lasers [3].Activate ();
	}
	private void ActivateLaser4() {
		lasers [4].Activate ();
	}

	private void DeactivateLaser() {
		current = LaserSetState.TO_BE_HIDDEN;
	}

}
