using UnityEngine;

/*
* generates coin and poweups
* on platform randomly.
*/

public class Platform : MonoBehaviour {

	public Vector3[] coinPositions;

	/**
	* set by platformManager to set Index
	* so that we can keep track of coins on each
	* platform.
	*/
	[HideInInspector]
	public int platformIndex;

	private Transform _transform;
	private CollectibleManager collectibleManager;
	private bool[] coinsData;

	void Awake() {
		_transform = GetComponent<Transform>();
		collectibleManager = CollectibleManager.Instance;
		platformIndex = -1;
	}

	void OnEnable() {
		if (platformIndex != -1) {
			coinsData = collectibleManager.GetCoinData(platformIndex);
			for (int i = 0; i < coinPositions.Length; i++) {
				if (coinsData[i]) {
					GameObject coin = collectibleManager.GetCoin();
					coin.transform.parent = _transform;
					SpawnCoin(coin, coinPositions[i]);
					coin.GetComponent<Coin>().SetIndex(i);
				}
			}
		}
	}

	void OnDisable() {
		if (platformIndex != -1) {
			collectibleManager.SaveCoinData(platformIndex, coinsData);

			// deactivate the coins attached on this platform.
			// i.e from second child, since first is sprites
			for (int i = 1; i < _transform.childCount -1; i++) {
				_transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

	/**
	* called by coin child to tell which has
	* fallen. So we set its state false, to not
	* show again.
	*/
	public void SetCoinState(int coinIndex) {
		coinsData[coinIndex] = false;
	}

	private void SpawnCoin(GameObject coin, Vector3 localPosition) {
		coin.transform.localPosition = localPosition;
		coin.transform.rotation = Quaternion.identity;
		coin.SetActive(true);
	}
}
