using UnityEngine;


public class PlatformDetector : MonoBehaviour {

    public bool isTopEdge = true;

    private PlatformManager platformManager;

    void Start() {
        platformManager = PlatformManager.Instance;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Platform")) {
            if (isTopEdge) {
                // add Platform
                platformManager.AddPlatform();
            } else {
                // remove Platform
                other.gameObject.SetActive(false);
            }
        }
    }
}
