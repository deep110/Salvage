using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour {

	private float[,] pattern = new float[,] {
		{0.0f, 1.5f, 3.0f, 4.5f, 6.0f},
		{0.0f, 0.0f, 0.0f, 2.0f, 2.0f},
		{0.0f, 2.0f, 0.0f, 2.0f, 0.0f},
	};

	private Laser[] lasers;

	void Start() {
		lasers = new Laser[5];
		//Initialize lasers
		for (int i = 0; i < transform.childCount; i++) {
			lasers [i] = transform.GetChild (i).GetComponent<Laser>();
		}
	}

	public float Activate() {
		//The first laser starts initialDelay seconds after it is enabled
		float initialDelay = 2f;
		int index = Random.Range (0, pattern.Length / transform.childCount);
		float duration = 0;
		for (int i = 0; i < transform.childCount; i++) {
			if (pattern [index, i] > duration) {
				duration = pattern [index, i];
			}
			print ("Invoke laser " + i + " : " + pattern [index, i] + initialDelay);
			Invoke ("ActivateLaser" + i, pattern [index, i] + initialDelay);
		}
		//for last laser will turn on till 2 sec then turn off + 1 sec wait before lasers disappear
		duration += 3;
		duration += initialDelay;
		print ("Invoke deactivate : " + duration);
		Invoke ("DeactivateLaser", duration);
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
		gameObject.SetActive (false);
	}

}
