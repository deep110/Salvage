using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour {

	private float[,] pattern = new float[,] {
		{0, 1.5f, 3, 4.5f, 6},
		{0, 0, 0, 2, 2},
		{0, 2, 0, 2, 0},
	};

	private Laser[] lasers;

	void Start() {
		lasers = new Laser[5];
		//Initialize lasers
		for (int i = 0; i < transform.childCount; i++) {
			lasers [i] = transform.GetChild (i).GetComponent<Laser>();
		}
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.L)) {
			Activate ();
		}
	}

	public void Activate() {
		print ("patter.Length : " + pattern.Length / transform.childCount);
		int index = Random.Range (0, pattern.Length / transform.childCount);
		for (int i = 0; i < transform.childCount; i++) {
			print ("index : " + index + " i : " + i);
			Invoke ("ActivateLaser" + i, pattern [index, i]);
		}
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

}
