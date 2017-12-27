using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

	public enum InputState {
		NONE, STILL, MOVE, JUMP
	};

	public float thresholdMove = 10f;

	private InputState current;
	private float deltaX;
	private float prevX, prevY;

	void Awake() {
		Application.targetFrameRate = 60;
	}

	void Start() {
		current = InputState.NONE;
		deltaX = 0;

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
			deltaX = touch.deltaPosition.x;

			switch (current) {
				case InputState.NONE:
					if (touch.phase == TouchPhase.Ended) {
						current = InputState.JUMP;
					} else if (touch.phase == TouchPhase.Moved) {
						if (Mathf.Abs(touch.deltaPosition.x) >= Mathf.Abs(touch.deltaPosition.y)) {
							current = InputState.MOVE;
						}
					}
					break;

				case InputState.JUMP:
					current = InputState.NONE;
					break;

				case InputState.STILL:
				case InputState.MOVE:
					switch (touch.phase) {
						case TouchPhase.Ended:
							current = InputState.NONE;
							break;
						case TouchPhase.Moved:
							current = (Mathf.Abs(touch.deltaPosition.x/Time.deltaTime)> thresholdMove)
								? InputState.MOVE : InputState.STILL;
							break;
						case TouchPhase.Stationary:
							current = InputState.STILL;
							break;
					}
					break;
			}
		} else {
			current = InputState.NONE;
			deltaX = 0;
		}
	}

	private void mapKeyBoardInput() {
		deltaX = Input.mousePosition.x - prevX;

		switch (current) {
			case InputState.NONE:
				float deltaY = Input.mousePosition.y - prevY;
				if (Input.GetMouseButtonUp(0)) {
					current = InputState.JUMP;
				} else if (Mathf.Abs(deltaX) >= Mathf.Abs(deltaY)
							&& Mathf.Abs(deltaX / Time.deltaTime) > thresholdMove) {
					current = InputState.MOVE;
				}
				break;

			case InputState.JUMP:
				current = InputState.NONE;
				break;

			case InputState.STILL:
				if (Input.GetMouseButtonUp(0)) {
					current = InputState.JUMP;
				} else {
					current = (Mathf.Abs(deltaX / Time.deltaTime) > thresholdMove)
						? InputState.MOVE : InputState.STILL;
				}
				break;
			case InputState.MOVE:
				if (Input.GetMouseButtonUp(0)) {
					current = InputState.NONE;
				} else {
					current = (Mathf.Abs(deltaX / Time.deltaTime) > thresholdMove)
						? InputState.MOVE : InputState.STILL;
				}
				break;
		}

		prevX = Input.mousePosition.x;
		prevY = Input.mousePosition.y;
	}

	public InputState GetCurrentState() {
		return current;
	}

	public float GetDeltaX() {
		return deltaX / Screen.width;
	}
}
