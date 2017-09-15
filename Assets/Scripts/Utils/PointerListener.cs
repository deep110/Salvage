using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class PointerListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public bool Pressed {get; private set;}
 
    public void OnPointerDown(PointerEventData eventData) {
        Pressed = true;
    }
 
    public void OnPointerUp(PointerEventData eventData) {
        Pressed = false;
    }
}
