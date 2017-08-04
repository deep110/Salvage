using UnityEngine;


public class PlatformDetector : MonoBehaviour {

    public bool isTopEdge = true;

	private Transform _transform;
	private PlatformManager platforManager;
	private float lastPosition;

	void Start() {
		_transform = GetComponent<Transform>();
        lastPosition = _transform.position.y;
        platforManager = _transform.parent.GetComponent<PlatformManager>();
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Platform")) {
        	if (_transform.position.y > lastPosition) {
                if (isTopEdge) {
    		        platforManager.AddPlatform(true);
                } else {
                    platforManager.RemovePlatform(false);
                }
        	} else {
                if (isTopEdge) {
                    platforManager.RemovePlatform(true);
                } else {
                    platforManager.AddPlatform(false);
                }
        	}

        	// update last position
        	lastPosition = _transform.position.y;
        }
    }
}