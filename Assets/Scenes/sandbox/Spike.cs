using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Enemy {

	public float bias;
	public float d;
	public float multiplier = 1;

	private enum State {
		CLOSE, CLOSE_UP, CLOSE_DOWN,
		OPEN, OPEN_UP, OPEN_DOWN
	};

	private float i;
	private State curr = State.CLOSE;
	private Vector2 position = Vector2.zero;
	private float t;
	private PolygonCollider2D collider;

	void Start () {
		i = 0.11f;
		position.x = transform.localPosition.x;

		transform.localPosition = position;
		collider = GetComponent<PolygonCollider2D> ();

		curr = State.OPEN_UP;

		isActive = true;
	}

	void Update () {

		switch (curr) {
		case State.OPEN_UP:
			if (t >= 0.8) {
				curr = State.OPEN_DOWN;
				t = 0.8f;
				position.y = t * (d + i) / 0.8f - bias;
				transform.localPosition = position;
			}
			position.y = t * (d + i) / 0.8f - bias;
			transform.localPosition = position;
			t += Time.deltaTime * multiplier;
			break;

		case State.OPEN_DOWN:
			position.y = i + (1 - t) * d / 0.2f - bias;
			transform.localPosition = position;
			t += Time.deltaTime * multiplier;
			if (t >= 1) {
				curr = State.OPEN;
				t = 1;
				position.y = i + (1 - t) * d / 0.2f - bias;
				transform.localPosition = position;
				t = 0;
			}
			break;

		case State.OPEN:

			break;

		case State.CLOSE_UP:
			if (t >= 0.2) {
				curr = State.CLOSE_DOWN;
				t = 0.2f;
				position.y = i + t * d * 5 - bias;
				transform.localPosition = position;
			}
			position.y = i + t * d * 5 - bias;
			transform.localPosition = position;
			t += Time.deltaTime * multiplier;
			break;

		case State.CLOSE_DOWN:
			position.y = (1 - t) * (d + i) / 0.8f - bias;
			transform.localPosition = position;
			t += Time.deltaTime * multiplier;
			if (t >= 1) {
				
				curr = State.CLOSE;
				t = 1;
				position.y = (1 - t) * (d + i) / 0.8f - bias;
				transform.localPosition = position;
				t = 0;
			}
			break;

		case State.CLOSE:
			break;
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.CompareTag ("Spike Trig")) {
			curr = State.CLOSE_UP;
			isActive = false;
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.CompareTag ("Spike Trig")) {
			curr = State.OPEN_UP;
			isActive = true;
		}
	}
}
