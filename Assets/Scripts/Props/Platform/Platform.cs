using UnityEngine;

///<summary>
/// generate coins on platform based on coin positions.
///</summary>
public class Platform : MonoBehaviour {

	public Vector3[] coinPositions;

	/// - unique index of platform
	/// - set by Platform Manager
	[HideInInspector]
	public int platformIndex;

	private Transform _transform;
	private CoinManager coinManager;
	private int numberCoinsFallen;

	void Awake() {
		_transform = GetComponent<Transform>();
		coinManager = CoinManager.Instance;
		platformIndex = -1;
	}

	void OnEnable() {
		if (platformIndex != -1) {
			for (int i = 0; i < coinPositions.Length; i++) {
				GameObject coin = coinManager.GetCoin();
				coin.transform.parent = _transform;
				coin.transform.localPosition = coinPositions[i];
				coin.transform.rotation = Quaternion.identity;
				coin.SetActive(true);
			}
		}
	}

	void OnDisable() {
		if (platformIndex != -1) {
			// deactivate the coins attached on this platform.
			for (int i = 0; i < _transform.childCount; i++) {
				if (_transform.GetChild(i).CompareTag("Coin")) {
					_transform.GetChild(i).gameObject.SetActive(false);
				}
			}

			// reset coins fallen
			numberCoinsFallen = 0;
		}
	}

	/// called by coin child to tell that it has been collected.
	/// It also checks if all coins on platform have been collected or not
	public void SetCoinFall() {
		numberCoinsFallen++;

		// Check if all platform coins are collected by the player
		if (numberCoinsFallen == 4) {
			EventManager.PlatformClear();
		}
	}
}
