using UnityEngine;

///<summary>
/// generate coins on platform based on coin positions.
///</summary>
public class Platform : MonoBehaviour {

    public Vector3[] crystalPositions;

    /// - unique index of platform
    /// - set by Platform Manager
    [HideInInspector]
    public int platformIndex;

    private Transform _transform;
    private PlatformManager platformManager;
    private int numberCoinsFallen;

    void Awake() {
        _transform = GetComponent<Transform>();
        platformManager = PlatformManager.Instance;
        platformIndex = -1;
    }

    void OnEnable() {
        if (platformIndex != -1) {
            for (int i = 0; i < crystalPositions.Length; i++) {
                GameObject crystal = platformManager.GetCrystal();
                crystal.transform.parent = _transform;
                crystal.transform.localPosition = crystalPositions[i];
                crystal.transform.rotation = Quaternion.identity;
                crystal.SetActive(true);
            }
        }
    }

    void OnDisable() {
        if (platformIndex != -1) {
            // deactivate the coins attached on this platform.
            for (int i = 0; i < _transform.childCount; i++) {
                if (_transform.GetChild(i).CompareTag("Crystal")) {
                    _transform.GetChild(i).gameObject.SetActive(false);
                }
            }

            // reset coins fallen
            numberCoinsFallen = 0;
            platformIndex = -1;
        }
    }

    /// called by coin child to tell that it has been collected.
    /// It also checks if all coins on platform have been collected or not
    public void SetCrystalFall() {
        numberCoinsFallen++;

        // Check if all platform coins are collected by the player
        if (numberCoinsFallen == 4) {
            EventManager.PlatformClear();
        }
    }
}
