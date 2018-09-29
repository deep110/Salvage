using UnityEngine;

///<summary>
/// generate crystals on platform based on crystal positions.
///</summary>
public class Platform : MonoBehaviour {

    public Vector3[] crystalPositions;

    private Transform _transform;
    private int numberCrystalFallen;

    private void Awake() {
        _transform = GetComponent<Transform>();
    }

    public void InitCrystals(PlatformManager platformManager) {
        for (int i = 0; i < crystalPositions.Length; i++) {
            GameObject crystal = platformManager.GetCrystal();
            crystal.transform.parent = _transform;
            crystal.transform.localPosition = crystalPositions[i];
            crystal.transform.rotation = Quaternion.identity;
            crystal.SetActive(true);
        }
    }

    /// called by crystal child to tell that it has been collected.
    /// It also checks if all crystals on platform have been collected or not
    public void SetCrystalFall() {
        numberCrystalFallen++;

        // Check if all platform crystals are collected by the player
        if (numberCrystalFallen == 4) {
            EventManager.PlatformClear();
        }
    }

    private void OnDisable() {
        // deactivate the crystals attached on this platform.
        for (int i = 0; i < _transform.childCount; i++) {
            if (_transform.GetChild(i).CompareTag("Crystal")) {
                _transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        // reset crystals fallen
        numberCrystalFallen = 0;
    }
}
