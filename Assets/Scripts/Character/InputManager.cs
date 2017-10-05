using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

	public enum InputState {
		NONE, LEFT, RIGHT, JUMP
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
		// check touch count and touch is not UI click
//		if (Input.touchCount > 0 && EventSystem.current.currentSelectedGameObject == null) {
//			Touch touch = Input.GetTouch(0);
//			if (touch.phase == TouchPhase.Ended && touch.tapCount == 1 && !dragging) {
//				pointerPos.y = Camera.main.ScreenToWorldPoint(touch.position).y;
//		        pointerClick = true;
//		        dragging = false;
//		    } else if (touch.phase == TouchPhase.Moved) {
//				pointerPos.x = touch.deltaPosition.x;
//				dragging = true;
//		    }
//		} else {
//			pointerClick = false;
//			dragging = false;
//			pointerPos.x = 0;
//		}
		if (Input.touchCount > 0 && EventSystem.current.currentSelectedGameObject == null) {
			Touch touch = Input.GetTouch (0);
			print ("touch phase : " + touch.phase.ToString());
			switch (current) {
			case InputState.NONE:
				if (touch.phase == TouchPhase.Ended) {
					current = InputState.JUMP;
				} else if (touch.phase == TouchPhase.Moved) {
					if (touch.deltaPosition.x > 0) {
						current = InputState.RIGHT;
					} else {
						current = InputState.LEFT;
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
			}
		} else {
			current = InputState.NONE;
			print ("Not touching");
		}
	}

	private void mapKeyBoardInput() {
		// check first if its not UI click
//		if (!EventSystem.current.IsPointerOverGameObject()) {
//			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//			pointerPos.x = mousePos.x;
//			pointerPos.y = mousePos.y;
//
//			pointerClick = Input.GetMouseButtonDown(0);
//		}
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
			} else if (Input.GetMouseButton (0) && deltaX != 0) {
				if (deltaX > 0) {
					current = InputState.RIGHT;
				} else {
					current = InputState.LEFT;
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
		}
	}

	public InputState getCurrentState() {
		return current;
	}
}
