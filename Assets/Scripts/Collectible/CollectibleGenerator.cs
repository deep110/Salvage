using UnityEngine;

/*
* generates coin and poweups
* on platform randomly.
*/

public class CollectibleGenerator : MonoBehaviour {

	public Vector3[] coinPositions;

	private CollectibleManager collectibleManager;
	private Transform coinsParent;

	private int coinsIndex;

	private void Awake() {
		collectibleManager = CollectibleManager.Instance;
		coinsParent = transform.Find("coins");
		coinsIndex = -1;
	}

	private void OnEnable () {
		for (int i=0; i < coinPositions.Length; i++) {
			GameObject coin = collectibleManager.GetCoin();
			coin.transform.parent = coinsParent;
			SpawnCoin(coin, coinPositions[i]);
		}
	}

	private void OnDisable() {

	}

	public void SetIndex(int index) {
		coinsIndex = index;
	}

	private void SpawnCoin(GameObject coin, Vector3 localPosition) {
		coin.transform.localPosition = localPosition;
		coin.transform.rotation = Quaternion.identity;
		coin.SetActive(true);
	}
}
