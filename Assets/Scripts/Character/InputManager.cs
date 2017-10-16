using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

	public enum InputState {
		NONE, STILL, LEFT, RIGHT, JUMP
	};

	private InputState current;

	[HideInInspector]
	public Vector2 pointerPos;

	[HideInInspector]
	public bool pointerClick;

	public float thresholdMove = 0.1f;

	float prevX, prevY;

	bool firstTouch = true;

	void Awake() {
		Application.targetFrameRate = 60;
	}

	void Start() {
		current = InputState.NONE;
	}

	void Update() {
		if (Application.isMobilePlatform) {
			mapMobileInput ();
		} else {
			mapKeyBoardInput ();
		}
	}

	private void mapMobileInput() {
		if (Input.touchCount > 0 && EventSystem.current.currentSelectedGameObject == null) {
			Touch touch = Input.GetTouch (0);
			switch (current) {
			case InputState.NONE:
				if (touch.phase == TouchPhase.Ended) {
					current = InputState.JUMP;
				} else if (touch.phase == TouchPhase.Moved) {
					if (Mathf.Abs (touch.deltaPosition.x) >= Mathf.Abs (touch.deltaPosition.y)) {
						if (touch.deltaPosition.x / Time.deltaTime > thresholdMove) {
							current = InputState.RIGHT;
						} else {
							current = InputState.LEFT;
						}
					}
				}
				break;
			case InputState.STILL:
				if (touch.phase == TouchPhase.Ended) {
					current = InputState.NONE;
				} else if (touch.phase == TouchPhase.Moved) {
					if (touch.deltaPosition.x / Time.deltaTime > thresholdMove) {
						current = InputState.RIGHT;
					} else if (touch.deltaPosition.x / Time.deltaTime < -thresholdMove) {
						current = InputState.LEFT;
					} else {
						current = InputState.STILL;
					}
				} else if (touch.phase == TouchPhase.Stationary) {
					current = InputState.STILL;
				}
				break;
			case InputState.JUMP:
				current = InputState.NONE;
				break;
			case InputState.LEFT:
				if (touch.phase == TouchPhase.Ended) {
					current = InputState.NONE;
				} else if (touch.phase == TouchPhase.Moved) {
					if (touch.deltaPosition.x / Time.deltaTime > thresholdMove) {
						current = InputState.RIGHT;
					} else if (touch.deltaPosition.x / Time.deltaTime < -thresholdMove) {
						current = InputState.LEFT;
					} else {
						current = InputState.STILL;
					}
				} else if (touch.phase == TouchPhase.Stationary) {
					current = InputState.STILL;
				}
				break;
			case InputState.RIGHT:
				if (touch.phase == TouchPhase.Ended) {
					current = InputState.NONE;
				} else if (touch.phase == TouchPhase.Moved) {
					if (touch.deltaPosition.x / Time.deltaTime > thresholdMove) {
						current = InputState.RIGHT;
					} else if (touch.deltaPosition.x / Time.deltaTime < -thresholdMove) {
						current = InputState.LEFT;
					} else {
						current = InputState.STILL;
					}
				} else if (touch.phase == TouchPhase.Stationary) {
					current = InputState.STILL;
				}
				break;
			}
		} else {
			current = InputState.NONE;
		}
	}

	private void mapKeyBoardInput() {
		if (firstTouch) {
			prevX = Input.mousePosition.x;
			prevY = Input.mousePosition.y;
			firstTouch = false;
		}
		float deltaX = Input.mousePosition.x - prevX;
		float deltaY = Input.mousePosition.y - prevY;
		prevX = Input.mousePosition.x;
		prevY = Input.mousePosition.y;


		switch (current) {
			case InputState.NONE:
				if (Input.GetMouseButtonUp (0)) {
					current = InputState.JUMP;
				} else if (Input.GetMouseButton (0) && !(deltaX == 0 && deltaY == 0)) {
					if (Mathf.Abs (deltaX) >= Mathf.Abs (deltaY)) {
						if (deltaX / Time.deltaTime > thresholdMove) {
							current = InputState.RIGHT;
						} else if (deltaX / Time.deltaTime < -thresholdMove) {
							current = InputState.LEFT;
						}
					}
				}
				break;
			case InputState.STILL:
				if (Input.GetMouseButtonUp (0)) {
					current = InputState.NONE;
				} else if (!Input.GetMouseButtonUp (0) && Input.GetMouseButton (0) && deltaX != 0) {
					if (deltaX / Time.deltaTime > thresholdMove) {
						current = InputState.RIGHT;
					} else if (deltaX / Time.deltaTime < -thresholdMove) {
						current = InputState.LEFT;
					} else {
						current = InputState.STILL;
					}
				} else {
					current = InputState.STILL;
				}
				break;
			case InputState.JUMP:
				current = InputState.NONE;
				break;
			case InputState.LEFT:
				if (Input.GetMouseButtonUp (0)) {
					current = InputState.NONE;
				} else if (!Input.GetMouseButtonUp (0) && Input.GetMouseButton (0) && deltaX != 0) {
					if (deltaX / Time.deltaTime > thresholdMove) {
						current = InputState.RIGHT;
					} else if (deltaX / Time.deltaTime < -thresholdMove) {
						current = InputState.LEFT;
					} else {
						current = InputState.STILL;
					}
				} else {
					current = InputState.STILL;
				}
				break;
			case InputState.RIGHT:
				if (Input.GetMouseButtonUp (0)) {
					current = InputState.NONE;
				} else if (!Input.GetMouseButtonUp (0) && Input.GetMouseButton (0) && deltaX != 0) {
					if (deltaX / Time.deltaTime < -thresholdMove) {
						current = InputState.LEFT;
					} else if (deltaX / Time.deltaTime > thresholdMove) {
						current = InputState.RIGHT;
					} else {
						current = InputState.STILL;
					}
				} else {
					current = InputState.STILL;
				}
				break;
		}
	}

	public InputState GetCurrentState() {
		return current;
	}
}
