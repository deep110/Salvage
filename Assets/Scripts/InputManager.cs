using UnityEngine;

public class InputManager : MonoBehaviour {

	[HideInInspector]
	public Vector2 pointerPos;

	[HideInInspector]
	public bool pointerClick;
	
	void Update () {

		if (Application.isMobilePlatform) {
			MapMobileInput ();
		} else {
			MapKeyBoardInput ();
		}
	}

	private void MapMobileInput() {
	}

	private void MapKeyBoardInput(){
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pointerPos.x = mousePos.x;
		pointerPos.y = mousePos.y;

		pointerClick = Input.GetMouseButtonDown(0);
	}
}
