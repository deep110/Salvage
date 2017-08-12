using UnityEngine;


public class PlatformDetector : MonoBehaviour {

    public bool isTopEdge = true;

	private Transform _transform;
	private PlatformManager platformManager;
	private float lastPosition;

	void Start() {
		_transform = GetComponent<Transform>();
        lastPosition = _transform.position.y;
        platformManager = PlatformManager.Instance;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Platform")) {
        	if (_transform.position.y > lastPosition) {
                if (isTopEdge) {
		            platformManager.AddPlatform(true);
                } else {
                    platformManager.RemovePlatform(false);
                }
        	} else {
                if (isTopEdge) {
                    platformManager.RemovePlatform(true);
                } else {
                    platformManager.AddPlatform(false);
                }
        	}

        	// update last position
        	lastPosition = _transform.position.y;
        }
    }
}
