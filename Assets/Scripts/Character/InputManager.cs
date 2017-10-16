using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

	public enum InputState {
		NONE, STILL, LEFT, RIGHT, JUMP
	};

	[HideInInspector]
	public Vector2 pointerPos;

	[HideInInspector]
	public bool pointerClick;

	public float thresholdMove = 10f;


	private InputState current;
	private float prevX, prevY;

	void Awake() {
		Application.targetFrameRate = 60;
	}

	void Start() {
		current = InputState.NONE;

		if (!Application.isMobilePlatform) {
			prevX = Input.mousePosition.x;
			prevY = Input.mousePosition.y;
		}
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
			Touch touch = Input.GetTouch(0);
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

				case InputState.JUMP:
					current = InputState.NONE;
					break;

				case InputState.STILL:
				case InputState.LEFT:
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
		float deltaX = Input.mousePosition.x - prevX;
		prevX = Input.mousePosition.x;
		prevY = Input.mousePosition.y;

		switch (current) {
			case InputState.NONE:
				float deltaY = Input.mousePosition.y - prevY;
				if (Input.GetMouseButtonUp (0)) {
					current = InputState.JUMP;
				} else if (Input.GetMouseButton(0)) {
					if (Mathf.Abs (deltaX) >= Mathf.Abs (deltaY)) {
						if (deltaX / Time.deltaTime > thresholdMove) {
							current = InputState.RIGHT;
						} else if (deltaX / Time.deltaTime < -thresholdMove) {
							current = InputState.LEFT;
						}
					}
				}
				break;

			case InputState.JUMP:
				current = InputState.NONE;
				break;

			case InputState.STILL:
			case InputState.LEFT:
			case InputState.RIGHT:
				if (Input.GetMouseButtonUp(0)) {
					current = InputState.NONE;
				} else if (Input.GetMouseButton(0)) {
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
		}
	}

	public InputState GetCurrentState() {
		return current;
	}
}
