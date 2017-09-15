using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

	[HideInInspector]
	public Vector2 pointerPos;

	[HideInInspector]
	public bool pointerClick;

	private bool dragging;
	
	void Update() {
		if (Application.isMobilePlatform) {
			mapMobileInput ();
		} else {
			mapKeyBoardInput ();
		}
	}

	private void mapMobileInput() {
		// check touch count and touch is not UI click
		if (Input.touchCount > 0 && EventSystem.current.currentSelectedGameObject == null) {
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Ended && touch.tapCount == 1 && !dragging) {
				pointerPos.y = Camera.main.ScreenToWorldPoint(touch.position).y;
		        pointerClick = true;
		        dragging = false;
		    } else if (touch.phase == TouchPhase.Moved) {
				pointerPos.x = touch.deltaPosition.x;
				dragging = true;
		    }
		} else {
			pointerClick = false;
			dragging = false;
			pointerPos.x = 0;
		}
	}

	private void mapKeyBoardInput() {
		// check first if its not UI click
		if (!EventSystem.current.IsPointerOverGameObject()) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pointerPos.x = mousePos.x;
			pointerPos.y = mousePos.y;

			pointerClick = Input.GetMouseButtonDown(0);
		}
	}
}
