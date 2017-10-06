using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

	public enum InputState {
		NONE, LEFT, RIGHT, JUMP, FALL
	};

	private InputState current;

	[HideInInspector]
	public Vector2 pointerPos;

	[HideInInspector]
	public bool pointerClick;

	float prevX = 0, prevY = 0;

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
			print ("touch phase : " + touch.phase.ToString());
			switch (current) {
			case InputState.NONE:
				if (touch.phase == TouchPhase.Ended) {
					current = InputState.JUMP;
				} else if (touch.phase == TouchPhase.Moved) {
					if (Mathf.Abs (touch.deltaPosition.x) >= Mathf.Abs (touch.deltaPosition.y)) {
						if (touch.deltaPosition.x > 0) {
							current = InputState.RIGHT;
						} else {
							current = InputState.LEFT;
						}
					} else {
						if (touch.deltaPosition.y < 0) {
							current = InputState.FALL;
						}
					}
				}
				break;
			case InputState.JUMP:
				current = InputState.NONE;
				break;
			case InputState.LEFT:
				if (touch.phase == TouchPhase.Ended) {
					current = InputState.NONE;
				} else if (touch.phase == TouchPhase.Moved) {
					if (touch.deltaPosition.x > 0) {
						current = InputState.RIGHT;
					}
				}
				break;
			case InputState.RIGHT:
				if (touch.phase == TouchPhase.Ended) {
					current = InputState.NONE;
				} else if (touch.phase == TouchPhase.Moved) {
					if (touch.deltaPosition.x < 0) {
						current = InputState.LEFT;
					}
				}
				break;
			
			case InputState.FALL:
				if (touch.phase == TouchPhase.Ended) {
					current = InputState.NONE;
				}
				break;
			}
		} else {
			current = InputState.NONE;
			print ("Not touching");
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
					if (deltaX > 0) {
						current = InputState.RIGHT;
					} else {
						current = InputState.LEFT;
					}
				} else {
					if (deltaY < 0) {
						current = InputState.FALL;
					}
				}
			}
			break;
		case InputState.JUMP:
			current = InputState.NONE;
			break;
		case InputState.LEFT:
			if (Input.GetMouseButtonUp (0)) {
				current = InputState.NONE;
			} else if (!Input.GetMouseButtonUp (0) && Input.GetMouseButton (0) && deltaX != 0) {
				if (deltaX > 0) {
					current = InputState.RIGHT;
				}
			}
			break;
		case InputState.RIGHT:
			if (Input.GetMouseButtonUp (0)) {
				current = InputState.NONE;
			} else if (!Input.GetMouseButtonUp (0) && Input.GetMouseButton (0) && deltaX != 0) {
				if (deltaX < 0) {
					current = InputState.LEFT;
				}
			}
			break;
		case InputState.FALL:
			if (Input.GetMouseButtonUp (0)) {
				current = InputState.NONE;
			}
			break;
		}
	}

	public InputState getCurrentState() {
		return current;
	}
}
