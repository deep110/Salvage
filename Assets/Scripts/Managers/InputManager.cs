using UnityEngine;

public class InputManager : MonoBehaviour {

	[HideInInspector]
	public Vector2 pointerPos;

	[HideInInspector]
	public bool pointerClick;

	private bool dragging;
	
	void Update () {

		if (Application.isMobilePlatform) {
			MapMobileInput ();
		} else {
			MapKeyBoardInput ();
		}
	}

	private void MapMobileInput() {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Ended && touch.tapCount == 1 && !dragging) {
				pointerPos.y = Camera.main.ScreenToWorldPoint(touch.position).y;
		        pointerClick = true;
		        dragging = false;
		    } else if (touch.phase == TouchPhase.Moved) {
				pointerPos.x = Camera.main.ScreenToWorldPoint(touch.position).x;
				dragging = true;
		    }
		} else {
			pointerClick = false;
			dragging = false;
		}
	}

	private void MapKeyBoardInput(){
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pointerPos.x = mousePos.x;
		pointerPos.y = mousePos.y;

		pointerClick = Input.GetMouseButtonDown(0);
	}
}
