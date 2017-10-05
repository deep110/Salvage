using UnityEngine;


public class PlatformDetector : MonoBehaviour {

    public bool isTopEdge = true;

	private Transform _transform;
	private PlatformManager platformManager;
	private float lastPosition;
    private bool addPlatform;

	void Start() {
		_transform = GetComponent<Transform>();
        lastPosition = _transform.position.y;
        platformManager = PlatformManager.Instance;
        addPlatform = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Platform")) {
        	if (_transform.position.y > lastPosition) {
                if (isTopEdge) {
                    if (addPlatform) {
		              platformManager.AddPlatform();
                    } else {
                        addPlatform = true;
                    }
                } else {
                    platformManager.RemovePlatform();
                }
            } else {
                addPlatform = false;
            }

        	// update last position
        	lastPosition = _transform.position.y;
        }
    }
}
