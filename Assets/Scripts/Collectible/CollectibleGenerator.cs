using UnityEngine;

/*
* generates coin and poweups
* on platform randomly.
*/

public class CollectibleGenerator : MonoBehaviour {

	public Vector3[] coinPositions;

	private Transform _transform;
	private CollectibleManager collectibleManager;

	private int platformIndex;
	private bool[] coinsData;

	void Awake() {
		_transform = GetComponent<Transform>();
		collectibleManager = CollectibleManager.Instance;
		platformIndex = -1;
	}

	void OnEnable () {
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
			for( int i=0; i< _transform.childCount; i++) {
				_transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

	/**
	* called by platformManager to set Index
	* so that we can keep track of coins on each 
	* platform.
	*/
	public void SetIndex(int platformIndex) {
		this.platformIndex = platformIndex;
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
